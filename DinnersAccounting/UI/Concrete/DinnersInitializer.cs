using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DA.Dinners.Domain;
using DA.Dinners.Domain.Concrete;
using DA.Dinners.Model;
using UI.Utils;

//using DA.Dinners.Domain.Concrete;

namespace UI.Concrete
{

    public class DinnersInitializer : DropCreateDatabaseIfModelChanges<DADinnersDomainContext>
    {

        protected override void Seed(DADinnersDomainContext context)
        {
            InitializingByTestValues(context);
        }

        private void InitializingByTestValues(DADinnersDomainContext context)
        {
            PersonRepository prep = new PersonRepository();

            Person p1 = new Person { DomainName = "UNIVERSE\\ikogan" };
            Person p4 = new Person { DomainName = "UNIVERSE\\bdudnik" };
            Person p5 = new Person { DomainName = "UNIVERSE\\kkarpenko" };

            Role role = new Role { RoleName = "Admin" };

            p4.Roles.Add(role);
            p5.Roles.Add(role);
            p1.Roles.Add(role);

            prep.InsertOrUpdate(p1);
            prep.InsertOrUpdate(p4);
            prep.InsertOrUpdate(p5);

            prep.Save();

            ContinuousProposition cp1 = new ContinuousProposition { StartDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday), EndDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(7) };
            cp1.Init();

            Product pd1 = new Product { Title = "Салат по - домашнему", Summary = "(помидоры, огурцы, масло растительное) 0,150", Price = 5.9M };
            Product pd2 = new Product { Title = "Борщ зелёный ", Summary = "0,300", Price = 8.44M };
            Product pd3 = new Product { Title = "Свекольник ", Summary = "0,300", Price = 9.09M };
            Product pd4 = new Product { Title = "Медальоны из телятины по - итальянски  ", Summary = "(на крутонах) 0,100", Price = 13.91M };

            Product k1 = new Product { Title = "Бизнес – комплекс № 2", Summary = "Капуста с помидорами 0,050 Суп \"Харчо\" 0,300 Сосиски отварные 0,100 Каша гречневая 0,200", Price = 21, isComplex = true };
            Product k2 = new Product { Title = "Комплекс – профессионал № 1", Summary = "Салат \"Дамский каприз\" (ветчина, капуста, помидоры, огурцы, майонез) 0,100 Суп \"Харчо\" 0,300 Эскалоп с помидорами 0,100 Картофель тушёный 0,200", Price = 25, isComplex = true };
            Product k3 = new Product { Title = "Комплекс – профессионал № 2", Summary = "Капуста с помидорами 0,050 Суп \"Харчо\" 0,300 Сосиски отварные 0,100 Каша гречневая 0,200", Price = 16, isComplex = true };

            cp1.Products.Add(pd1);
            cp1.Products.Add(pd2);

            foreach (var DayProposition in cp1.DayPropositions)
            {
                DayProposition.Products.Add(pd1.Clone());
                DayProposition.Products.Add(pd4.Clone());
                DayProposition.Products.Add(k1.Clone());
                DayProposition.Products.Add(k2.Clone());
                DayProposition.Products.Add(k3.Clone());
            }

            ContinuousProposition cp2 = new ContinuousProposition { StartDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(8), EndDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(15) };
            cp2.Init();
            cp2.Products.Add(pd4.Clone());
            cp2.Products.Add(pd3.Clone());
            foreach (var DayProposition in cp2.DayPropositions)
            {
                DayProposition.Products.Add(pd2.Clone());
                DayProposition.Products.Add(pd4.Clone());
                DayProposition.Products.Add(k1.Clone());
                DayProposition.Products.Add(k2.Clone());
                DayProposition.Products.Add(k3.Clone());
            }

            ContinuousProposition cp3 = new ContinuousProposition { StartDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(16), EndDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(23) };
            cp3.Init();
            cp3.Products.Add(pd1.Clone());
            cp3.Products.Add(pd4.Clone());

            foreach (var DayProposition in cp3.DayPropositions)
            {
                DayProposition.Products.Add(pd3.Clone());
                DayProposition.Products.Add(pd4.Clone());
                DayProposition.Products.Add(k1.Clone());
                DayProposition.Products.Add(k2.Clone());
                DayProposition.Products.Add(k3.Clone());
            }

            PropositionRepository repo = new PropositionRepository();
            repo.InsertOrUpdate(cp1);
            repo.InsertOrUpdate(cp2);
            repo.InsertOrUpdate(cp3);
            repo.Save();
        }
    }

    
}

