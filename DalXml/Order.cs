using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Xml.Linq;
using System.Runtime.Intrinsics.Arm;

namespace Dal
{

    public struct ImportentNumbers
    {
        public double numberSaved { get; set; }
        public string typeOfnumber { get; set; }
    }

    internal class Order : IOrder
    {

        const string s_orders = @"Orders";
        string configPath = @"config.xml";

        public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
        {
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
            return filter == null ? ordersList.OrderBy(lec => ((DO.Order)lec!).ID)
                                  : ordersList.Where(filter).OrderBy(lec => ((DO.Order)lec!).ID);
        }

        public DO.Order GetById(int id) =>
            XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(p => p?.ID == id)
            //DalMissingIdException(id, "Lecturer");
            ?? throw new Exception("missing id");

        public Order GetById(Func<Order?, bool>? predicate)
        {
            return XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefaultr(predicate);
            //    ?? throw new NotExists("Sorry ,this order does not exist in the List ");
        }

        public int Add(DO.Order order)
        {
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

            if (ordersList.Exists(lec => lec?.ID == order.ID))
                throw new Exception("id already exist");//DalAlreadyExistIdException(lecturer.ID, "Lecturer");

            List<ImportentNumbers> runningList = XMLTools.LoadListFromXMLSerializer<ImportentNumbers>(configPath);

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == "Order Running Number")
                                           select number).FirstOrDefault();
            runningList.Remove(runningNum);

            runningNum.numberSaved++;
            order.ID = (int)runningNum.numberSaved;


            runningList.Add(runningNum);
            ordersList.Add(order);

            XMLTools.SaveListToXMLSerializer(runningList, configPath);
            XMLTools.SaveListToXMLSerializer(ordersList, s_orders);

            return order.ID;
        }

        public void Delete(int id)
        {
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

            if (ordersList.RemoveAll(p => p?.ID == id) == 0)
                throw new Exception("missing id"); //new DalMissingIdException(id, "Lecturer");

            XMLTools.SaveListToXMLSerializer(ordersList, s_orders);
        }

        public void Update(DO.Order order)
        {
            Delete(order.ID);
            Add(order);
        }

    }
}
