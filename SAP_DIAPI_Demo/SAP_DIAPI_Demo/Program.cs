using Newtonsoft.Json;
using SAP_DIAPI_Demo.Configurations;
using SAP_DIAPI_Demo.Infrastructure;
using SAP_DIAPI_Demo.Infrastructure.SAP.Factories;
using SAP_DIAPI_Demo.Interfaces.Repository;
using SAP_DIAPI_Demo.Interfaces.Services;
using SAP_DIAPI_Demo.Models;
using SAP_DIAPI_Demo.Repositories;
using SAP_DIAPI_Demo.Repository;
using SAP_DIAPI_Demo.Services;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace SAP_DIAPI_Demo.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Khởi tạo Dependencies thông qua Factory (Không dùng từ khóa "new" cho hạ tầng cụ thể nữa)
            IBusinessPartnerRepository bpRepo = SapRepositoryFactory.CreateBusinessPartnerRepository();
            IBusinessPartnerService bpService = new BusinessPartnerService(bpRepo);

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=============================================");
                Console.WriteLine("   SAP INTEGRATION SYSTEM - INTERACTIVE TOOL ");
                Console.WriteLine("=============================================");

                // Hiển thị trạng thái cấu hình hiện tại từ App.config
                Console.ForegroundColor = AppSetting.UseServiceLayer ? ConsoleColor.Magenta : ConsoleColor.Blue;
                Console.WriteLine($"   [CURRENT MODE]: {(AppSetting.UseServiceLayer ? "SERVICE LAYER (REST API)" : "DI API (COM OBJECT)")}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=============================================");
                Console.ResetColor();

                Console.WriteLine("1. Update Business Partner");
                Console.WriteLine("0. Exit Program");
                Console.WriteLine("---------------------------------------------");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HandleUpdateBP(bpService);
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Exiting... Goodbye!");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void HandleUpdateBP(IBusinessPartnerService service)
        {
            Console.WriteLine("\n--- Update Business Partner ---");
            Console.Write("Enter CardCode to update: ");
            string code = Console.ReadLine();
            Console.Write("Enter New Name: ");
            string name = Console.ReadLine();

            // Khởi tạo Model chuẩn từ Domain
            var model = new BusinessPartnerModel
            {
                CardCode = code,
                CardName = name
            };

            // Gọi Service thực thi logic nghiệp vụ
            var res = service.UpdateBusinessPartner(model);

            ShowResult(res.Success, res.Message);
        }

        static void ShowResult(bool success, string msg, string data = "")
        {
            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                // Bỏ đi khoảng trắng thừa nếu biến data rỗng
                string dataDisplay = string.IsNullOrEmpty(data) ? "" : $" | Data: {data}";
                Console.WriteLine($"SUCCESS: {msg}{dataDisplay}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"FAILED: {msg}");
            }
            Console.ResetColor();
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    }




}