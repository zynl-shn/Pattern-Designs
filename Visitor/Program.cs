﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Manager engin = new Manager()
            {
                Name = "Engin",Salary = 1000
            };

            Manager salih = new Manager()
            {
                Name = "Salih", Salary = 900
            };

            Worker derin = new Worker()
            {
                Name = "Derin", Salary = 800
            };

            Worker ali = new Worker()
            {
                Name = "Ali",Salary = 700
            };


            engin.SubOrdinates.Add(salih);
            salih.SubOrdinates.Add(derin);
            salih.SubOrdinates.Add((ali));


            OrganisationalStructure organisationalStructure = new OrganisationalStructure(engin);

            PayrollVisitor payrollVisitor = new PayrollVisitor();
            PayriseVisitor payriseVisitor = new PayriseVisitor();
            organisationalStructure.Accept(payriseVisitor);
            organisationalStructure.Accept(payrollVisitor);

            Console.ReadKey();
        }

    }

    class OrganisationalStructure
    {
        public EmployeeBase Employee;

        public OrganisationalStructure(EmployeeBase firstEmployee)
        {
            Employee = firstEmployee;
        }

        public void Accept(VisitorBase visitor)
        {
            Employee.Accept(visitor);
        }
    }

    abstract class EmployeeBase
    {
        public abstract void Accept(VisitorBase visitor);

        public string Name { get; set; }
        public decimal Salary { get; set; }
    }


    class Manager:EmployeeBase
    {
        public Manager()
        {
            SubOrdinates = new List<EmployeeBase>();
        }
        public List<EmployeeBase> SubOrdinates { get; set; }
        
        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);
            foreach (var employee in SubOrdinates)
            {
                employee.Accept(visitor);
            }
        }
    }

    class Worker:EmployeeBase
    {
        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);
        }
    }
    abstract class VisitorBase
    {
        public abstract void Visit(Worker worker);
        public abstract void Visit(Manager manager);
    }

    class PayrollVisitor:VisitorBase
    {
        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} paid {1}",worker.Name,worker.Salary);
        }

        public override void Visit(Manager manager)
        {

            Console.WriteLine("{0} paid {1} ",manager.Name,manager.Salary);
        }
    }

    class PayriseVisitor:VisitorBase
    {
        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} salary increased to {1}", worker.Name, worker.Salary*(decimal) 1.1);
        }

        public override void Visit(Manager manager)
        {
            Console.WriteLine("{0} salary increased to {1} ", manager.Name, manager.Salary *(decimal)1.2);
        }
    }
}
