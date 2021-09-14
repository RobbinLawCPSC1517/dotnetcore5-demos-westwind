using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fields_properties
{
    class Student
    {
        public string name;  // a public field
        public int gradeField; //a public field
        private int _gradeField1; // a private field

        public int getGrade1()
        {
            Console.WriteLine("getGrade1");
            return _gradeField1;
        }
        public void setGrade1(int number)
        {
            Console.WriteLine("setGrade1");
            if (number == 34)
                throw new Exception("setGrade1 Bad Input");
            _gradeField1 = number;
        }
        // a private field
        private int _gradeField2;
        // a public fully implemented property. Use for validation.
        public int PropFullImp 
        {
            get 
            { 
                Console.WriteLine("PropFullImp getter"); 
                return _gradeField2; 
            }
            set 
            { 
                Console.WriteLine("PropFullImp setter");
                if(value == 45)
                    throw new Exception("PropFullImp setter Bad Input"); 
                _gradeField2 = value; 
            }
        }
        
        // a public auto implemented property: No backing field required as auto set up
        public int PropAutoImp { get; set; }

        public override string ToString()
        {
            return $"Name: {name}, gradeField: {gradeField}, gradeField1: {getGrade1()}, PropFullImp: {PropFullImp}, PropAutoImp: {PropAutoImp}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var newStudent = new Student();

                newStudent.name = "John";   
                newStudent.gradeField = 22;      
                newStudent.setGrade1(33); 
                newStudent.PropFullImp = 44;
                if(newStudent.PropFullImp == 45)
                    throw new Exception("Main PropFullImp Bad Input"); 
                newStudent.PropAutoImp = 55;
                if(newStudent.PropAutoImp == 55)
                    throw new Exception("Main PropAutoImp Bad Input"); 
                Console.WriteLine(newStudent.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
