using CSV.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSV.Models.Utilities;
using System.Text;
using System.Xml;
using System.Drawing;

namespace CSV
{
    class Program
    {

        static void Main(string[] args)
        {
            int i;
            List<string> directories;
            string[] directory;
            Student student = new Student();
            List<Student> studentlist = new List<Student>();
            List<String> entries = new List<string>();
            String error = null;
            String FilePath;


            int a = 0;
            while (a != 10)
            {
                Console.WriteLine("\n1. Directories \n2. Extract Data From Your Directory " +
                     "\n3. Add Your Record From Student Object \n4. Student Information From My Directory" +
                     "\n5. Display Result of Student Object List \n6. Generate CSV File " +
                     "\n7. Generate JSON File \n8. Generate XML File \n9. Exit");
                Console.WriteLine("\nEnter The Number: ");
                a = Convert.ToInt32(Console.ReadLine());
                if (a == 1)
                {
                    Console.WriteLine("\n 1. Directories ");
                    directories = FTP.GetDirectory(Constants.FTP.BaseUrl);
                    if (!directories.Any())
                    {
                        Console.WriteLine("Directory is Empty");
                    }
                    else
                    {
                        foreach (var Directory in directories)
                        {
                            Console.WriteLine(Directory.TrimStart());
                        }
                    }
                }

                else if (a == 2)
                {
                    i = 0;
                    while (i != 4)
                    {
                        Console.WriteLine("\n 2. Extract Data From Directories ");
                        Console.WriteLine("\n1. Get Name of the Files from Data Folders \n2. Read Data From info.csv File  \n3. Convert Your image into" +
                        "Base64 and add to info.CSV File \n4. Exit");
                        Console.WriteLine("\nEnter The Number: ");

                        i = Convert.ToInt32(Console.ReadLine());

                        if (i == 1)
                        {
                            Console.WriteLine(" 1. Files ");
                            string[] getFileList = Directory.GetFiles($@"{Constants.Locations.DataFolder}");
                            foreach (string files in getFileList)
                            {
                                Console.WriteLine("Files in Data Folder: " + Path.GetFileName(files));
                            }
                            getFileList = Directory.GetFiles($@"{Constants.Locations.ImageFolder}");
                            foreach (string files in getFileList)
                            {
                                Console.WriteLine("Files in Image Folder: " + Path.GetFileName(files));
                            }
                        }

                        else if (i == 2)
                        {
                            Console.WriteLine("======== 2. Read Data From info.csv File ========");
                            FilePath = Constants.Locations.InfoFilePath;
                            String fileContents;
                            using (StreamReader stream = new StreamReader(FilePath))
                            {
                                fileContents = stream.ReadToEnd();
                            }
                            entries = fileContents.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                            Console.WriteLine(entries[1]);
                        }

                        else if (i == 3)
                        {
                            Console.WriteLine("======== 3.Convert Your image into Base64 and add to info.CSV File ========");
                            FilePath = Constants.Locations.InfoFilePath;
                            String fileContents;
                            using (StreamReader stream = new StreamReader(FilePath))
                            {
                                fileContents = stream.ReadToEnd();
                            }
                            entries = fileContents.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                            String[] s = entries[1].ToString().Split(',');
                            if (s.Length == 5)
                            {
                                Console.WriteLine("There is already Image Data value in CSV File");
                            }
                            else
                            {
                                Console.WriteLine("======== Convert Image Data into Base64 and add to info.csv File ========");
                                Image image = Image.FromFile(Constants.Locations.ImageFilePath);
                                string base64Image = imaging.ImageToBase64(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                                var csv = File.ReadLines(Constants.Locations.InfoFilePath).Select((line, index) => index == 0 ? line + ",Image"
                                : line + "," + base64Image).ToList();
                                File.WriteAllLines(Constants.Locations.InfoFilePath, csv);
                                Console.WriteLine("Image Data Entered Successfully");

                            }
                        }
                    }
                }

                else if (a == 3)
                {
                    i = 0;
                    while (i != 4)
                    {
                        Console.WriteLine("\n 3. Adding Your Record From Student Object ");
                        Console.WriteLine("\n1. Adding Student Record from info.csv file to Student Object" +
                            " \n2. Student Object as String Representation  \n3. Student Object AS CSV Representation \n4. Exit");
                        Console.WriteLine("\nEnter The Number: ");
                        i = Convert.ToInt32(Console.ReadLine());

                        if (i == 1)
                        {
                            Console.WriteLine("\n 1. Adding Student Record from info.csv file to Student Object ");
                            FilePath = Constants.Locations.InfoFilePath;
                            String fileContents;
                            using (StreamReader stream = new StreamReader(FilePath))
                            {
                                fileContents = stream.ReadToEnd();
                            }
                            entries = fileContents.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                            student.FromCSV(entries[1]);
                            Console.WriteLine("Data Input Successfully");

                        }

                        else if (i == 2)
                        {
                            Console.WriteLine("\n 2. Student Object as String Representation ");
                            if (student.StudentId == null)
                            {
                                Console.WriteLine("No Record Found");
                            }
                            else
                            {
                                Console.WriteLine(student.ToString());
                            }
                        }

                        else if (i == 3)
                        {
                            Console.WriteLine("\n 3. Student Object AS CSV Representation ");
                            if (student.StudentId == null)
                            {
                                Console.WriteLine("No Record Found");
                            }
                            else
                            {
                                Console.WriteLine(student.ToCSV());
                            }
                        }
                    }
                }

                else if (a == 4)
                {
                    i = 0;
                    while (i != 5)
                    {
                        Console.WriteLine("\n 4. Student Information From My Directory ");
                        Console.WriteLine("\n1. List Of Student info Files in Data Folder" +
                            " \n2. Retrive student Information From FTP to My Directory " +
                            " \n3. Student Information using ToString Method" +
                            "\n4. Student Information using ToCSV Method \n5. Exit");
                        Console.WriteLine("\nEnter Your Choice Number: ");

                        i = Convert.ToInt32(Console.ReadLine());
                        if (i == 1)
                        {
                            Console.WriteLine("\n 1. List Of Student info Files in Local Data Folder ");
                            directory = Directory.GetFiles(Constants.Locations.StudentDataFolder);
                            if (!directory.Any())
                            {
                                Console.WriteLine("Folder is Empty");
                            }
                            else
                            {
                                foreach (var Directory in directory)
                                {
                                    Console.WriteLine(Path.GetFileName(Directory).TrimStart());
                                }
                            }
                        }
                        else if (i == 2)
                        {
                            Console.WriteLine("\n 2. Retrive student Information From FTP to My Directory ");
                            directories = FTP.GetDirectory(Constants.FTP.BaseUrl);
                            foreach (var Directory in directories)
                            {
                                student = new Student() { AbsoluteUrl = Constants.FTP.BaseUrl };
                                student.FromDirectory(Directory);
                                string infoPath = student.FullPathUrl + "/" + Constants.Locations.InfoFile;
                                bool fileExist = FTP.FileExists(infoPath);
                                Console.WriteLine(student);
                                if (fileExist == true)
                                {
                                    string csvPath = $@"{Constants.Locations.StudentDataFolder}\{Directory}.csv";
                                    FTP.DownloadFile(infoPath, csvPath);
                                    byte[] bytes = FTP.DownloadFileBytes(infoPath);
                                    string csvData = Encoding.Default.GetString(bytes);
                                    string[] csvlines = csvData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                                    if (csvlines.Length != 2)
                                    {
                                        Console.WriteLine("Error in CSV Format");
                                    }
                                    else
                                    {
                                        student.FromCSV(csvlines[1]);
                                    }
                                    Console.WriteLine("Found info File");
                                }
                                else
                                {
                                    Console.WriteLine("Could not find info file:");
                                }
                                Console.WriteLine("\t" + infoPath);
                                string imageFilePath = student.FullPathUrl + "/" + Constants.Locations.ImageFile;
                                string imagePath = $@"{Constants.Locations.StudentImageFolder}\{Directory}.jpeg";
                                FTP.DownloadFile(imageFilePath, imagePath);
                                bool fileExist1 = FTP.FileExists(imageFilePath);
                                if (fileExist1 == true)
                                {
                                    Console.WriteLine("Found image File");
                                }
                                else
                                {
                                    Console.WriteLine("Could not find the file:");
                                }
                                Console.WriteLine("\t" + imageFilePath);
                            }
                        }

                        else if (i == 3)
                        {
                            string[] getFileList = Directory.GetFiles($@"{Constants.Locations.StudentDataFolder}");
                            foreach (string files in getFileList)
                            {
                                Student tempstudent = new Student();
                                entries = new List<string>();
                                FilePath = Constants.Locations.InfoFilePath;
                                String fileContents;
                                using (StreamReader stream = new StreamReader(FilePath))
                                {
                                    fileContents = stream.ReadToEnd();
                                }
                                entries = fileContents.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                                student.FromCSV(entries[1]);
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

                            Console.WriteLine("\n 3. Student Information using ToString Method ");
                            foreach (var student1 in studentlist)
                            {
                                Console.WriteLine(student1.ToString());
                            }

                        }

                        else if (i == 4)
                        {
                            string[] getFileList = Directory.GetFiles($@"{Constants.Locations.StudentDataFolder}");
                            foreach (string files in getFileList)
                            {
                                Student tempstudent = new Student();
                                entries = new List<string>();
                                FilePath = Constants.Locations.InfoFilePath;
                                String fileContents;
                                using (StreamReader stream = new StreamReader(FilePath))
                                {
                                    fileContents = stream.ReadToEnd();
                                }
                                entries = fileContents.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                                student.FromCSV(entries[1]);
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

                            Console.WriteLine("\n 4. Student Information using ToCSV Method ");
                            foreach (var student1 in studentlist)
                            {
                                Console.WriteLine(student1.ToCSV());
                            }
                        }
                    }
                }

                else if (a == 5)
                {
                    i = 0;
                    int age;
                    while (i != 8)
                    {
                        string[] getFileList = Directory.GetFiles($@"{Constants.Locations.StudentDataFolder}");
                        foreach (string files in getFileList)
                        {
                            Student tempstudent = new Student();
                            entries = new List<string>();
                            FilePath = Constants.Locations.InfoFilePath;
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
                        Console.WriteLine("\n 5. Display Result of Student Object List ");
                        Console.WriteLine("\n1. Count Number of Students in the List" +
                            " \n2. Start With '2004' Files Name " +
                            " \n3. Show Files that Contains 'Patel' " +
                            "\n4. Record \n5. Average Age \n6. Highest Age \n7. Lowest Age \n8. Exit");
                        Console.WriteLine("\nEnter Your Choice Number: ");

                        i = Convert.ToInt32(Console.ReadLine());
                        directory = Directory.GetFiles(Constants.Locations.StudentDataFolder);

                        if (i == 1)
                        {
                            Console.WriteLine("\n 1. Count Number of Students in the List ");
                            Console.WriteLine(directory.Length);
                        }

                        else if (i == 2)
                        {
                            Console.WriteLine("\n 2. Start With '200450' Files Name ");
                            String startValue = "2004";
                            Console.WriteLine("File Starts With '2004':");
                            foreach (var dir in directory)
                            {
                                String str = Path.GetFileName(dir);
                                if (str.StartsWith(startValue))
                                {
                                    Console.WriteLine(str);
                                }
                            }
                        }

                        else if (i == 3)
                        {
                            Console.WriteLine("\n 3. Show Files that Contains 'Patel' ");
                            String Contains = "Patel";
                            foreach (var dir in directory)
                            {
                                String str = Path.GetFileName(dir);
                                if (str.Contains(Contains))
                                {
                                    Console.WriteLine(str);
                                }
                            }
                        }

                        else if (i == 4)
                        {

                            Console.WriteLine("\n 4. Records ");
                            foreach (var stu in studentlist)
                            {
                                Structure.MyRecord(stu);
                                if (stu.MyRecord == true)
                                {
                                    Console.WriteLine(stu);
                                    break;
                                }

                            }
                        }

                        else if (i == 5)
                        {
                            Console.WriteLine("\n 5. Average Age ");
                            age = 0;
                            i = 0;
                            foreach (var stu in studentlist)
                            {
                                if (stu.Age < 80 & stu.Age > 0)
                                {
                                    age = age + stu.Age;
                                }
                                else
                                {
                                    i = i + 1;
                                    Console.WriteLine("Error in File");
                                    Console.WriteLine("Student Record\n" + stu.ToString() + "\nDOB : " + stu.DateOfBirthDT.ToShortDateString());
                                }

                            }
                            i = studentlist.Count - i;
                            //Console.WriteLine("Total Age of " + temp + " people is : " + age);
                            Console.WriteLine("\nAverage Age of " + i + " people is :" + age / i);
                        }


                        else if (i == 6)
                        {
                            Console.WriteLine("\n 6. Highest Age ");
                            age = 0;

                            foreach (var stu in studentlist)
                            {
                                if (stu.Age < 80 & stu.Age > 0)
                                {
                                    if (age < stu.Age)
                                    {
                                        age = stu.Age;
                                        student = stu;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error in File");
                                    Console.WriteLine("Student Record\n" + stu.ToString() + "\nDOB : " + stu.DateOfBirthDT.ToShortDateString());
                                }

                            }
                            Console.WriteLine();
                            Console.WriteLine("\nHighest Age - " + age);
                        }

                        else if (i == 7)
                        {
                            Console.WriteLine("\n 7. Lowest Age ");
                            age = 100;
                            foreach (var stu in studentlist)
                            {
                                if (stu.Age < 80 && stu.Age > 0)
                                {
                                    if (age > stu.Age)
                                    {
                                        age = stu.Age;
                                        student = stu;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error in File");
                                    Console.WriteLine("Student Record\n" + stu.ToString() + "\nDOB : " + stu.DateOfBirthDT.ToShortDateString());
                                }

                            }
                            Console.WriteLine();
                            Console.WriteLine("\nLowest Age - " + age);
                        }
                    }
                }

                else if (a == 6)
                {

                    Console.WriteLine(" 6. Generating CSV File ");
                    string[] getFileList = Directory.GetFiles($@"{Constants.Locations.StudentDataFolder}");
                    studentlist = new List<Student>();
                    foreach (string files in getFileList)
                    {
                        Student tempstudent = new Student();
                        entries = new List<string>();
                        FilePath = Constants.Locations.InfoFilePath;
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
                    using (StreamWriter fs = new StreamWriter(Constants.Locations.StudentCSVPath))
                    {
                        foreach (var stu in studentlist)
                        {
                            fs.WriteLine(stu.ToCSV());
                        }
                    }
                    Console.WriteLine("\nFile Created");
                    Console.WriteLine(FTP.UploadFile(Constants.Locations.StudentCSVPath, Constants.FTP.BaseUrl + Constants.Locations.remoteUploadCsvFileDestination));
                    Console.WriteLine("Error Found in Below File\n" + error);

                }

                else if (a == 7)
                {
                    Structure.JSON();
                    Console.WriteLine(FTP.UploadFile(Constants.Locations.StudentJSONPath, Constants.FTP.BaseUrl + Constants.Locations.remoteUploadCsvFileDestination));
                }


                else if (a == 8)
                {
                    Structure.XML();
                    Console.WriteLine(FTP.UploadFile(Constants.Locations.StudentCSVPath, Constants.FTP.BaseUrl + Constants.Locations.remoteUploadXmlFileDestination));
                }


            }
        }
    }
}




