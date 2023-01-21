using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

//implement IProduct with linq to XML
namespace Dal
{

    public struct ImportentNumbers
    {
        public double numberSaved { get; set; }
        public string typeOfnumber { get; set; }
    }

    internal class OrderItem : IOrderItem
    {
       
        const string s_orderItems = @"OrderItems";
        string configPath = @"config";

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

        public DO.OrderItem? GetById(Func<DO.OrderItem?, bool>? predicate)
        {
            var orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
            if(orderItemList.FirstOrDefault(predicate)==null)
            {
                throw new Exception("Order Item not Exsits");
            }
           return orderItemList.FirstOrDefault(predicate);
            
        }

        public int Add(DO.OrderItem orderItem)
        {
            var orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

            if (orderItemsList.Exists(lec => lec?.ID == orderItem.ID))
                throw new Exception("id already exist");//DalAlreadyExistIdException(lecturer.ID, "Lecturer");

            List<ImportentNumbers> runningList = XMLTools.LoadListFromXMLSerializer1<ImportentNumbers>(configPath);

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == "OrderItem Running Number")
                                           select number).FirstOrDefault();
            runningList.Remove(runningNum);

            runningNum.numberSaved++;
            orderItem.ID = (int)runningNum.numberSaved;


            runningList.Add(runningNum);
            orderItemsList.Add(orderItem);

            XMLTools.SaveListToXMLSerializer1(runningList, configPath);
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
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            if (ordersList.Exists(item => item?.ID == orderItem.ID))
                throw new Exception("id already exist");
            ordersList.Add(orderItem);
            XMLTools.SaveListToXMLSerializer(ordersList, s_orderItems);
        }

    }
}

