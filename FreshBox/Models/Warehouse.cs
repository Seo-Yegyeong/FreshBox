//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using FreshBox.Enums;

//namespace FreshBox.Models
//{
//    public class Warehouse
//    {
//        public int Id { get; set; }
//        public string Location { get; set; } = string.Empty;
//        public StorageTemp TempControl { get; set; }

//        public Warehouse(int id, string location, string tempControl) {
//            Id = id;
//            Location = location;

//            switch (tempControl)
//            {
//                case "냉장":
//                    TempControl = StorageTemp.냉장;
//                    break;
//                case "냉동":
//                    TempControl = StorageTemp.냉동;
//                    break;
//                case "실온":
//                    TempControl = StorageTemp.실온;
//                    break;
//                default:
//                    throw new Exception("Error: 잘못된 옵션입니다.");
//            }
//        }
//    }
//}
