using System;
using System.IO;

/*
K-Nearest Neighbor alogirthm on a part of the Iris flower data set.
Classes:
- 0 = Setosa
- 1 = Virginica
- 2 = Versicolor
*/

namespace CSIT496_KNN
{
    class KNNalgorithm
    {
        static void Main(string[] args)
        {
            //path to csv file
            string path = @"C:\Users\Tyler\OneDrive\Desktop\Programming - Copy\CSIT496_KNN\CSIT496_KNN\bin\FlowerData.csv";

            //where we will store our data from CSV
            double[,] data = new double[15, 4];

            if (File.Exists(path))//check file is there
            {
                StreamReader reader = new StreamReader(path);
                int entries = 0;//used to access all of the 2d array

                while (!reader.EndOfStream)
                {
                    string temp = reader.ReadLine();
                    string[] tempArr = temp.Split(",");

                    data[entries, 0] = double.Parse(tempArr[0]);
                    data[entries, 1] = double.Parse(tempArr[1]);
                    data[entries, 2] = double.Parse(tempArr[2]);

                    entries++;
                }

                //sample data we are trying to classify
                double length = 7;
                double width = 3.1;

                for (int i = 0; i < entries; i++)
                    data[i, 3] = getDistance(length, width, data[i, 0], data[i, 1]);

                //order matrix by distance
                for (int i = 0; i < entries; i++)
                {
                    for (int j = 0; j < entries; j++)
                    {
                        if (data[i, 3] < data[j, 3])
                        {
                            //then switch the places of distance(3) and class(2)
                            double temp = data[j, 3];
                            double classTemp = data[j, 2];
                            data[j, 3] = data[i, 3];
                            data[j, 2] = data[i, 2];
                            data[i, 3] = temp;
                            data[i, 2] = classTemp;
                        }
                    }
                }

                //Kvalue for amount of neighbors
                int Kvalue = 5;

                //Array for count of each class that appears
                int[] dataClass = new int[3];

                for (int i = 0; i < Kvalue; i++)
                    dataClass[(int)data[i, 2]]++;//Count the Kvalue amount of closest neighbor's class

                int highestCount = getHighestClassCount(dataClass);
               

                if (highestCount == 0)
                    Console.WriteLine("Sample classified as Setosa.");
                if (highestCount == 1)
                    Console.WriteLine("Sample classified as Virginica.");
                if (highestCount == 2)
                    Console.WriteLine("Sample classified as Verscicolor.");

                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Couldn't find file.");
                Console.ReadLine();
            } 
        }

        //gets distance between two points
        public static double getDistance(double Length, double width, double att1, double att2)
        {
            return Math.Sqrt(Math.Pow((att1 - Length), 2) + Math.Pow((att2 - width), 2));
        }

        //returns which index has the largest int
        public static int getHighestClassCount(int[] classes)
        {
            if (classes[0] > classes[1] && classes[0] > classes[2])
                return 0;
            if (classes[1] > classes[0] && classes[1] > classes[2])
                return 1;
            if (classes[2] > classes[1] && classes[2] > classes[0])
                return 2;
            return -1;
        }
    }
}
