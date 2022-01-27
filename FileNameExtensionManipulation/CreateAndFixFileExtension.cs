//  -------------------------------------------------------------------------
//  <copyright file="CreateAndFixFileExtension.cs"  author="Rajesh Thomas | iamrajthomas" >
//      Copyright (c) 2022 All Rights Reserved.
//  </copyright>
// 
//  <summary>
//       CreateAndFixFileExtension
//  </summary>
//  -------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileNameExtensionManipulation
{
    class CreateAndFixFileExtension
    {
        /// <summary>
        /// TestHereProblem
        /// </summary>
        public static void TestHereProblem()
        {
            var filePath = @"default:\\agreements\\ICMAutomationBasicAgreement_679ee1ec-10cc-42fd-bf3c-d629a63c3047_1.Pdf";

            var fileName = "TestAGGR_RT04062021_J.D..Docx"; // With double quotes and period //THIS IS NOT WORKING \\ ERROR


            //---------------------------- WORKING STARTED
            //var fileName = "TestAGGR_RT04062021_JD.Docx"; // Without Any Special Characters //THIS IS WORKING
            //var fileName = "TestAGGR_RT04062021_\'JD\'.Docx"; // With single quotes //THIS IS WORKING
            //var fileName = "TestAGGR_RT04062021_.J.D.Docx"; // With periods and No Double Quotes //THIS IS WORKING
            //---------------------------- WORKING ENDED

            //---------------------------- NOT WORKING STARTED
            // The issue is with Double Quotes
            //var fileName = "TestAGGR_RT04062021_\"JD\".Docx"; // With double quotes //THIS IS NOT WORKING // ERROR
            //var fileName = "TestAGGR_RT04062021_\"J.D.\".Docx"; // With double quotes and period //THIS IS NOT WORKING // ERROR
            //---------------------------- NOT WORKING ENDED

            var extension = string.IsNullOrWhiteSpace(filePath) ? string.Empty : Path.GetExtension(filePath).Split('.')[1];

            var updatedfileName = fileName.Replace(Path.GetExtension(fileName).Split('.')[1], extension);
            Console.ReadLine();
        }

        /// <summary>
        /// TestHereSolution1 
        /// Here the file name in output will contain Single Quotes if present in Input
        /// </summary>
        public static void TestHereSolution1()
        {
            var filePath = @"default:\\agreements\\ICMAutomationBasicAgreement_679ee1ec-10cc-42fd-bf3c-d629a63c3047_1.Pdf";

            var fileName = "DocxTestAGGR_RT04062021\"J.D.\".Docx"; // With double quotes and period //THIS IS WORKING 

            //var updatedfileName = fileName.Replace(Path.GetExtension(fileName).Split('.')[1], extension);


            //---------------------------- WORKING STARTED
            //var fileName = "TestAGGR_RT04062021_JD.Docx"; // Without Any Special Characters //THIS IS WORKING
            //var fileName = "TestAGGR_RT04062021_\'JD\'.Docx"; // With single quotes //THIS IS WORKING
            //var fileName = "TestAGGR_RT04062021_.J.D.Docx"; // With periods and No Double Quotes //THIS IS WORKING
            //var fileName = "TestAGGR_RT04062021_\"J.D.\".Docx"; // With double quotes and period //THIS IS WORKING 
            //var fileName = "TestAGGR_RT04062021_\"JD\".Docx"; // With double quotes only //THIS IS WORKING
            //---------------------------- WORKING ENDED


            //---------------------------- NOT WORKING STARTED
            //---------------------------- NOT WORKING ENDED


            var extension = string.IsNullOrWhiteSpace(filePath) ? string.Empty : Path.GetExtension(filePath).Split('.')[1];
            var updatedfileName = string.Empty;
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                //TEST
                //var path = Path.GetExtension(fileName); //THIS IS THE CULPRIT //Here the Path.GetExtension cannot resolve fileName

                var regfileName = System.Text.RegularExpressions.Regex.Replace(fileName, @"[^0-9a-zA-Z.']+", "");
                var fileNameSplits = Path.GetExtension(regfileName).Split('.');
                var filepathExtension = fileNameSplits[fileNameSplits.Length - 1];
                updatedfileName = fileName.Replace(filepathExtension, extension);

                fileName = Path.ChangeExtension(System.Text.RegularExpressions.Regex.Replace(fileName, @"[^0-9a-zA-Z.']+", ""), extension);
            }


            updatedfileName = fileName.Replace(Path.GetExtension(fileName).Split('.')[1], extension);


            Console.WriteLine(updatedfileName);
            Console.ReadLine();
        }


        /// <summary>
        /// TestHereSolution2
        /// Here the file name in output will contain only valid characters
        /// This is More Accurate Solution than Solution 1
        /// </summary>
        public static void TestHereSolution2()
        {
            var filePath = @"default:\\agreements\\ICMAutomationBasicAgreement_679ee1ec-10cc-42fd-bf3c-d629a63c3047_1.Pdf";

            var fileName = "Docx+(-=@#$%&Test'AG'GR_RT04062021\"J.D.\").Docx"; // With double quotes and period //THIS IS WORKING 

            //---------------------------- WORKING STARTED
            //var fileName = "TestAGGR_RT04062021_JD.Docx"; // Without Any Special Characters //THIS IS WORKING
            //var fileName = "TestAGGR_RT04062021_\'JD\'.Docx"; // With single quotes //THIS IS WORKING
            //var fileName = "TestAGGR_RT04062021_.J.D.Docx"; // With periods and No Double Quotes //THIS IS WORKING
            //var fileName = "TestAGGR_RT04062021_\"J.D.\".Docx"; // With double quotes and period //THIS IS WORKING 
            //var fileName = "TestAGGR_RT04062021_\"JD\".Docx"; // With double quotes only //THIS IS WORKING
            //---------------------------- WORKING ENDED


            //---------------------------- NOT WORKING STARTED
            //---------------------------- NOT WORKING ENDED


            var extension = string.IsNullOrWhiteSpace(filePath) ? string.Empty : Path.GetExtension(filePath).Split('.')[1];

            //var updatedfileName = !string.IsNullOrEmpty(fileName) ? Path.ChangeExtension(Regex.Replace(fileName, @"[^0-9a-zA-Z.']+", ""), extension) : string.Empty;
            //var updatedfileName = !string.IsNullOrEmpty(fileName) ? Path.ChangeExtension(Regex.Replace(fileName, @"[^0-9a-zA-Z_,.']+", ""), extension) : string.Empty;


            var updatedfileName = !string.IsNullOrEmpty(fileName) ? Path.ChangeExtension(Regex.Replace(fileName, @"[^0-9a-zA-Z_=+-,.@#$%&'()]+", string.Empty), extension) : string.Empty;
            var updatedfileName2 = !string.IsNullOrEmpty(fileName) ? Path.ChangeExtension(Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => Regex.Replace(current, @"[^0-9a-zA-Z_=+-,.@#$%&'()]+", string.Empty)), extension) : string.Empty;
            var updatedfileName3 = !string.IsNullOrEmpty(fileName) ? Path.ChangeExtension(Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty)), extension) : string.Empty;

            var data = Path.GetInvalidFileNameChars();

            var ifBothSolutionsAreSame = false;
            if (updatedfileName == updatedfileName2 && updatedfileName2 == updatedfileName3)
            {
                ifBothSolutionsAreSame = true;
            }

            Console.WriteLine(updatedfileName);
            Console.WriteLine(updatedfileName2);
            Console.WriteLine(updatedfileName3);
            Console.WriteLine("If Both The Solutions Are Same: {0}", ifBothSolutionsAreSame);
            Console.ReadLine();
        }
    }
}
