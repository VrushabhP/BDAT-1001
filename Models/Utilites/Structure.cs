using CSV.Models.Utilities;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CSV.Models
{
    public class Structure
    {
       
        public static void JSON()
        {
            
            List<Student> studentlist = new List<Student>();
            String error = null; 
            List<Student.Student_Data> student_Data = new List<Student.Student_Data>();
            string[] getFileList = Directory.GetFiles($@"{Constants.Locations.StudentDataFolder}");
            studentlist = new List<Student>();
            foreach (string files in getFileList)
            {
                Student tempstudent = new Student();
                List<String> entries = new List<string>();
                String FilePath = Constants.Locations.InfoFilePath;
                String fileContents;
                using (StreamReader stream = new StreamReader(FilePath))
                {
                    fileContents = stream.ReadToEnd();
                }
                entries = fileContents.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                try
                {
                    tempstudent.FromCSV(entries[1]);
                    studentlist.Add(tempstudent);
                }
                catch
                {
                    error = error + Path.GetFileName(files) + "\n";
                }
            }
            try
            {

                foreach (var c in studentlist)
                {
                    Student.Student_Data obj = new Student.Student_Data
                    {
                        _StudentID = $"{c.StudentId}",
                        _FirstName = $"{c.FirstName}",
                        _LastName = $"{c.LastName}",
                        _DateOfBirth = $"{c.DateOfBirthDT.ToShortDateString()}",
                        _ImageData = $"{c.ImageData}"
                    };
                    student_Data.Add(obj);
                }
                using (StreamWriter file = File.CreateText(Constants.Locations.StudentJSONPath))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                    serializer.Serialize(file, student_Data);
                }
                Console.WriteLine("\nJSON File Created\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void XML()
        {
            List<Student> studentlist = new List<Student>();
            String error = null;
            List<Student.Student_Data> student_Data = new List<Student.Student_Data>();
            string[] getFileList = Directory.GetFiles($@"{Constants.Locations.StudentDataFolder}");
            studentlist = new List<Student>();
            foreach (string files in getFileList)
            {
                Student tempstudent = new Student();
                List<String> entries = new List<string>();
                String FilePath = Constants.Locations.InfoFilePath;
                String fileContents;
                using (StreamReader stream = new StreamReader(FilePath))
                {
                    fileContents = stream.ReadToEnd();
                }
                entries = fileContents.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                try
                {
                    tempstudent.FromCSV(entries[1]);
                    studentlist.Add(tempstudent);
                }
                catch
                {
                    error = error + Path.GetFileName(files) + "\n";
                }
            }
            try
            {
                XmlTextWriter writer = new XmlTextWriter(Constants.Locations.StudentXMLPath, System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = System.Xml.Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("Student_Info");
                foreach (var stu in studentlist)
                {
                    stu.ToXML(writer);
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
                Console.WriteLine("\nXML File Created\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static (List<Student>, String) SetInfoData(String getFileList)
        {
            string error = null;
            var result = getFileList.Split('.').Last();
            List<Student> studentlist = new List<Student>();
            if (result == "csv")
            {
                String fileContents;
                using (StreamReader stream = new StreamReader(getFileList))
                {
                    fileContents = stream.ReadToEnd();
                }
                List<String> entries = fileContents.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                
                foreach (var x in entries)
                {
                    try
                    {
                        Student tempstudent = new Student();
                        tempstudent.FromCSV(x);
                        studentlist.Add(tempstudent);
                    }
                    catch
                    {
                        error = error + Path.GetFileName(getFileList) + "\n";
                    }
                }

            }
            
            return (studentlist, error);
        }

        //Set "MyRecord" parameter True for my record
        public static void MyRecord(Student stu)
        {
            Constants cons = new Constants();
            if (stu.StudentId == cons.Student.StudentId)
            {
                stu.MyRecord = true;
            }
            else
            {
                stu.MyRecord = false;
            }
        }
    }
}

