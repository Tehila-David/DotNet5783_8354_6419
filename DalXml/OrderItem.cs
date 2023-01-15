using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//implement IStudent with linq to XML
namespace Dal
{


    internal class OrderItem : IOrderItem
    {


        const string s_orderItems = @"OrderItem";
        string configPath = @"config.xml";

        public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
        {
            var orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
            return filter == null ? orderItemsList.OrderBy(lec => ((DO.OrderItem)lec!).ID)
                                  : orderItemsList.Where(filter).OrderBy(lec => ((DO.OrderItem)lec!).ID);
        }

        public DO.OrderItem GetById(int id) =>
            XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems).FirstOrDefault(p => p?.ID == id)
            //DalMissingIdException(id, "Lecturer");
            ?? throw new Exception("missing id");

        public OrderItem GetById(Func<DO.OrderItem?, bool>? predicate)
        {
            //return XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems).FirstOrDefault(predicate);
            ////    ?? throw new NotExists("Sorry ,this order does not exist in the List ");
        }

        public int Add(DO.OrderItem orderItem)
        {
            var orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

            if (orderItemsList.Exists(lec => lec?.ID == orderItem.ID))
                throw new Exception("id already exist");//DalAlreadyExistIdException(lecturer.ID, "Lecturer");

            List<ImportentNumbers> runningList = XMLTools.LoadListFromXMLSerializer<ImportentNumbers>(configPath);

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == "OrderItem Running Number")
                                           select number).FirstOrDefault();
            runningList.Remove(runningNum);

            runningNum.numberSaved++;
            orderItem.ID = (int)runningNum.numberSaved;


            runningList.Add(runningNum);
            orderItemsList.Add(orderItem);

            XMLTools.SaveListToXMLSerializer(runningList, configPath);
            XMLTools.SaveListToXMLSerializer(orderItemsList, s_orderItems);

            return orderItem.ID;
        }

        public void Delete(int id)
        {
            var orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

            if (orderItemsList.RemoveAll(p => p?.ID == id) == 0)
                throw new Exception("missing id"); //new DalMissingIdException(id, "Lecturer");

            XMLTools.SaveListToXMLSerializer(orderItemsList, s_orderItems);
        }

        public void Update(DO.OrderItem orderItem)
        {
            Delete(orderItem.ID);
            Add(orderItem);
        }

    }
}

