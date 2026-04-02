using SAPbouiCOM.Framework;
using System;

namespace SAP_Integration.Addon
{
    [FormAttribute("SAP_Integration.Addon.Form1", "Form1.b1f")]
    class Form1 : UserFormBase
    {
        public Form1() { }

        // 1. Khai báo các biến thành viên (Nên đặt tên gợi nhớ để dễ code)
        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.EditText txtFrom;
        private SAPbouiCOM.EditText txtTo;
        private SAPbouiCOM.EditText txtCard;

        /// <summary>
        /// Khởi tạo Component - Nơi kết nối ID từ Designer vào Code
        /// </summary>
        public override void OnInitializeComponent()
        {
            //  Kết nối Grid và Button
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("grdData").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("btnLoad").Specific));
            //  Kết nối các ô lọc dữ liệu (Đảm bảo ID trong Designer khớp với chuỗi này)
            this.txtFrom = ((SAPbouiCOM.EditText)(this.GetItem("txtFrom").Specific));
            this.txtTo = ((SAPbouiCOM.EditText)(this.GetItem("txtTo").Specific));
            this.txtCard = ((SAPbouiCOM.EditText)(this.GetItem("txtCard").Specific));
            //  Đăng ký sự kiện Click cho nút bấm
            this.Button0.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button0_ClickAfter);
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("stFrom").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("txtFrom").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("stTo").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("txtTo").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("stCard").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("txtCard").Specific));
            this.EditText6.KeyDownAfter += new SAPbouiCOM._IEditTextEvents_KeyDownAfterEventHandler(this.EditText6_KeyDownAfter);
            this.OnCustomInitialize();

        }

        public override void OnInitializeFormEvents() {
            this.LoadAfter += new LoadAfterHandler(this.Form_LoadAfter);

        }

        private void OnCustomInitialize() { }

        /// <summary>
        /// Logic xử lý chính khi bấm nút Load
        /// </summary>
        private void Button0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                // 1. Đóng băng màn hình để tránh lag
                UIAPIRawForm.Freeze(true);

                // 2. Lấy giá trị tham số từ giao diện
                string fromDate = txtFrom.Value; // Định dạng YYYYMMDD
                string toDate = txtTo.Value;
                string cardCode = txtCard.Value;

                // 3. Gọi Repository từ tầng Infrastructure (Project Core)
                var repository = new SAP_Integration.Core.infrastructure.repository.SalesReportRepository();
                string sqlCall = repository.GetSalesFilterReportQuery(fromDate, toDate, cardCode);

                // 4. Xử lý DataTable của Form
                SAPbouiCOM.DataTable oDT;
                try
                {
                    oDT = UIAPIRawForm.DataSources.DataTables.Item("DT_Rpt");
                }
                catch
                {
                    oDT = UIAPIRawForm.DataSources.DataTables.Add("DT_Rpt");
                }

                // 5. Thực thi câu lệnh CALL Store Procedure
                oDT.ExecuteQuery(sqlCall);

                // 6. Đổ dữ liệu vào Grid
                Grid0.DataTable = oDT;

                // 7. Định dạng hiển thị cho Grid
                FormatGrid();

                Application.SBO_Application.StatusBar.SetText("Tải báo cáo thành công!",
                    SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
            catch (Exception ex)
            {
                // Nếu lỗi thì phải nhả Freeze ngay để UI không bị đơ
                UIAPIRawForm.Freeze(false);
                Application.SBO_Application.MessageBox("Lỗi thực thi: " + ex.Message);
            }
            finally
            {
                // Luôn luôn nhả Freeze ở đây
                UIAPIRawForm.Freeze(false);
            }
        }

        /// <summary>
        /// Hàm trang trí Grid: Đổi tên cột, tạo Link tới Master Data
        /// </summary>
        private void FormatGrid()
        {
            if (Grid0.DataTable.Rows.Count == 0) return;

            // Đổi tên tiêu đề cột cho chuyên nghiệp
            Grid0.Columns.Item("DocNum").TitleObject.Caption = "Số đơn hàng";
            Grid0.Columns.Item("DocDate").TitleObject.Caption = "Ngày chứng từ";
            Grid0.Columns.Item("CardCode").TitleObject.Caption = "Mã khách hàng";
            Grid0.Columns.Item("CardName").TitleObject.Caption = "Tên khách hàng";
            Grid0.Columns.Item("LineTotal").TitleObject.Caption = "Thành tiền";

            // Tạo link (Mũi tên vàng) để nhấn vào mã khách mở thẳng Master Data
            var colCard = (SAPbouiCOM.EditTextColumn)Grid0.Columns.Item("CardCode");
            colCard.LinkedObjectType = "2";

            // Tự động căn chỉnh độ rộng cột
            Grid0.AutoResizeColumns();

            // Không cho phép sửa trực tiếp trên Grid này (vì là báo cáo)
        }

        private void Form_LoadAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {

        }

        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.EditText EditText6;

        private void EditText6_KeyDownAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            throw new System.NotImplementedException();

        }
    }
}
