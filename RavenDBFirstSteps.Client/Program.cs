using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Document;
using RavenDBFirstSteps.Client.Model;

namespace RavenDBFirstSteps.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Tea tea = new Tea() { Name = "Earl Grey", TeaType = TeaType.Black, WaterTemp = 99, SleepTime = 3 };

            StoreTea(tea);

            Console.WriteLine(tea.Id + "--" + tea.Name);

            tea = LoadTea("teas/1");

            Console.WriteLine(tea.Id + "--" + tea.Name);

            foreach (Tea dbTea in ListTeasByType(TeaType.Black))
            {
                Console.WriteLine(dbTea.Name);
            }

            Console.ReadLine();
        }

        static void StoreTea(Tea tea)
        {
            using (var ds = new DocumentStore { Url = DATABASEURL }.Initialize())
            {
                using (var session = ds.OpenSession(DATABASE))
                {
                    session.Store(tea);
                    session.SaveChanges();
                    Console.WriteLine(tea.Id);
                }
            }
        }

        static Tea LoadTea(String id)
        {
            using (var ds = new DocumentStore { Url = DATABASEURL }.Initialize())
            {
                using (var session = ds.OpenSession(DATABASE))
                {
                    return session.Load<Tea>(id);
                }
            }
        }

        static void DeleteTea(String id)
        {
            using (var ds = new DocumentStore { Url = DATABASEURL }.Initialize())
            {
                using (var session = ds.OpenSession(DATABASE))
                {
                    Tea tea = session.Load<Tea>(id);
                    session.Delete(tea);
                    session.SaveChanges();
                }
            }
        }

        static List<Tea> ListTeasByType(TeaType teaType)
        {
            using (var ds = new DocumentStore { Url = DATABASEURL }.Initialize())
            {
                using (var session = ds.OpenSession(DATABASE))
                {
                    List<Tea> dbTeas = (from teas in session.Query<Tea>()
                                        where teas.TeaType == teaType
                                        select teas).ToList<Tea>();

                    return dbTeas;
                }
            }
        }

        const String DATABASE = "RavenDBFirstSteps";
        const String DATABASEURL = "http://localhost:8080/";

    }
}
