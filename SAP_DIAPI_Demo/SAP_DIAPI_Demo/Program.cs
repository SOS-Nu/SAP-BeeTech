using SAP_DIAPI_Demo.Configurations;
using SAP_DIAPI_Demo.Infrastructure.SAP.Factories;
using SAP_DIAPI_Demo.Interfaces.Repository;
using SAP_DIAPI_Demo.Interfaces.Services;
using SAP_DIAPI_Demo.Models;
using SAP_DIAPI_Demo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Khởi tạo Dependencies thông qua Factory
            IBusinessPartnerRepository bpRepo = SapRepositoryFactory.CreateBusinessPartnerRepository();
            IBusinessPartnerService bpService = new BusinessPartnerService(bpRepo);

            IItemRepository itemRepo = SapRepositoryFactory.CreateItemRepository();
            IItemService itemService = new ItemService(itemRepo);

            // Khởi tạo thêm Service cho Sales Order
            ISalesOrderRepository orderRepo = SapRepositoryFactory.CreateSalesOrderRepository();
            ISalesOrderService orderService = new SalesOrderService(orderRepo);

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=============================================");
                Console.WriteLine("   SAP INTEGRATION SYSTEM - INTERACTIVE TOOL ");
                Console.WriteLine("=============================================");

                Console.ForegroundColor = AppSetting.UseServiceLayer ? ConsoleColor.Magenta : ConsoleColor.Blue;
                Console.WriteLine($"   [CURRENT MODE]: {(AppSetting.UseServiceLayer ? "SERVICE LAYER (REST API)" : "DI API (COM OBJECT)")}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=============================================");
                Console.ResetColor();

                Console.WriteLine("1. Create Sales Order");
                Console.WriteLine("2. Update Business Partner");
                Console.WriteLine("3. Update Item Price (Master Data)");
                Console.WriteLine("0. Exit Program");
                Console.WriteLine("---------------------------------------------");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HandleCreateOrder(orderService);
                        break;
                    case "2":
                        HandleUpdateBP(bpService);
                        break;
                    case "3":
                        HandleUpdateItemPrice(itemService);
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

        // --- HÀM XỬ LÝ TẠO SALES ORDER (NHIỀU DÒNG) ---
        static void HandleCreateOrder(ISalesOrderService orderService)
        {
            Console.WriteLine("\n--- Create Sales Order ---");
            Console.Write("Enter CardCode: ");
            string cardCode = Console.ReadLine();

            Console.Write("Enter Branch ID (BPL_ID): ");
            int.TryParse(Console.ReadLine(), out int bplId);

            Console.Write("Enter Comments: ");
            string comments = Console.ReadLine();

            var orderModel = new SalesOrderModel
            {
                CardCode = cardCode,
                DocDueDate = DateTime.Now.AddDays(1), // Mặc định ngày mai giao hàng
                BPL_IDAssignedToInvoice = bplId,
                Comments = comments,
                Lines = new List<SalesOrderItemModel>()
            };

            // Vòng lặp nhập Line Items
            bool addingLines = true;
            while (addingLines)
            {
                Console.WriteLine($"\n--- Item Line #{orderModel.Lines.Count + 1} ---");
                Console.Write("Enter ItemCode: ");
                string itemCode = Console.ReadLine();
                if (string.IsNullOrEmpty(itemCode)) break;

                Console.Write("Enter Quantity: ");
                double.TryParse(Console.ReadLine(), out double qty);

                Console.Write("Enter Price (Optional - Enter to use SAP default): ");
                string priceInput = Console.ReadLine();
                double? price = null;
                if (!string.IsNullOrEmpty(priceInput))
                {
                    if (double.TryParse(priceInput, out double p)) price = p;
                }

                Console.Write("Enter Warehouse: ");
                string whs = Console.ReadLine();

                orderModel.Lines.Add(new SalesOrderItemModel
                {
                    ItemCode = itemCode,
                    Quantity = qty,
                    Price = price ?? 0,
                    WarehouseCode = whs
                });

                Console.Write("Add another item? (y/n): ");
                addingLines = Console.ReadLine()?.ToLower() == "y";
            }

            if (orderModel.Lines.Count == 0)
            {
                Console.WriteLine("Order must have at least one line. Operation cancelled.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Sending to SAP...");
            var res = orderService.CreateOrder(orderModel);

            // Hiển thị kết quả kèm DocNum/DocEntry nếu thành công
            string extraData = res.Data != null ? $"DocEntry: {res.Data.DocEntry} | DocNum: {res.Data.DocNum}" : "";
            ShowResult(res.Success, res.Message, extraData);
        }

        static void HandleUpdateBP(IBusinessPartnerService bpService)
        {
            Console.WriteLine("\n--- Update Business Partner ---");
            Console.Write("Enter CardCode to update: ");
            string code = Console.ReadLine();
            Console.Write("Enter New Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Phone1: ");
            string phone = Console.ReadLine();

            var model = new BusinessPartnerModel
            {
                CardCode = code,
                CardName = name,
                Phone1 = phone
            };

            var res = bpService.UpdateBusinessPartner(model);
            ShowResult(res.Success, res.Message);
        }

        static void HandleUpdateItemPrice(IItemService itemService)
        {
            Console.WriteLine("\n--- Update Item Price ---");
            Console.Write("Enter ItemCode: ");
            string itemCode = Console.ReadLine();
            Console.Write("Enter PriceList ID: ");
            int.TryParse(Console.ReadLine(), out int priceList);
            Console.Write("Enter New Price: ");
            double.TryParse(Console.ReadLine(), out double price);
            Console.Write("Enter Currency: ");
            string currency = Console.ReadLine();

            var model = new ItemModel
            {
                ItemCode = itemCode,
                PriceList = priceList,
                Price = price,
                Currency = currency
            };

            var res = itemService.UpdatePriceItem(model);
            ShowResult(res.Success, res.Message);
        }

        static void ShowResult(bool success, string msg, string data = "")
        {
            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string dataDisplay = string.IsNullOrEmpty(data) ? "" : $" | {data}";
                Console.WriteLine($"\nSUCCESS: {msg}{dataDisplay}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nFAILED: {msg}");
            }
            Console.ResetColor();
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    }
}