# Yêu cầu project phần mềm giám sát nhiệt độ hóa chất SEMV

## 1. Thông tin tài liệu

| Nội dung | Giá trị |
|---|---|
| Tên project | Phần mềm giám sát nhiệt độ hóa chất SEMV |
| Loại ứng dụng | Windows Forms desktop application |
| Nền tảng đề xuất | .NET 10 LTS, Windows x64 |
| Số điểm giai đoạn đầu | 21 tank |
| Khả năng mở rộng | Tối thiểu 40 tank |
| Giao tiếp thiết bị | RS485, Modbus RTU |
| Ngày lập tài liệu | 07/10/2026 |
| Trạng thái | Bản yêu cầu ban đầu, cần xác nhận trước khi phát triển |

## 2. Tài liệu đầu vào

Project được xây dựng dựa trên các tài liệu sau:

1. [Phương án thi công giám sát hóa chất SEMV.docx](./Phương%20án%20thi%20công%20giám%20sát%20hóa%20chất%20SEMV.docx)
2. [High-performance-PID-temperature-controllers-AUTONICS-TK-series-MANUAL-228.pdf](./High-performance-PID-temperature-controllers-AUTONICS-TK-series-MANUAL-228.pdf)
3. Hướng dẫn TK Series của Autonics: https://www.autonics.com/glb/data/manual/en/TK
4. Chương trình mẫu C# cho TK Series của Autonics: https://www.autonics.com/us/service/data/view/9/247455

Tài liệu PDF hiện có là instruction manual, chưa chứa bảng địa chỉ thanh ghi Modbus đầy đủ. Trước khi hoàn thành driver giao tiếp cần có thêm communication user manual, register map chính thức hoặc chương trình mẫu C# của Autonics.

## 3. Mục tiêu project

Phần mềm phải giúp nhân viên vận hành:

- Theo dõi nhiệt độ của toàn bộ tank trên một màn hình tập trung.
- Nhận biết nhanh tank bình thường, quá nhiệt, nhiệt độ thấp, lỗi sensor hoặc mất kết nối.
- Nhận và xác nhận cảnh báo.
- Lưu lịch sử nhiệt độ và lịch sử cảnh báo.
- Tra cứu dữ liệu theo thời gian, tank, hóa chất và loại sự kiện.
- Xuất dữ liệu phục vụ báo cáo và kiểm tra.
- Quản lý cấu hình tank, ngưỡng cảnh báo và kết nối thiết bị.
- Mở rộng từ 21 lên ít nhất 40 tank mà không phải thay đổi kiến trúc chính.

Phần mềm là lớp giám sát. Đèn, còi và các liên động an toàn tại hiện trường phải có khả năng hoạt động độc lập khi máy tính, phần mềm hoặc đường truyền RS485 gặp sự cố.

## 4. Phạm vi phiên bản 1

### 4.1 Chức năng nằm trong phạm vi

- Ứng dụng Windows Forms chạy trên máy tính tại phòng giám sát.
- Giám sát 21 tank và cấu hình mở rộng đến 40 tank.
- Kết nối một hoặc nhiều tuyến RS485 Modbus RTU.
- Đọc nhiệt độ hiện tại PV từ từng bộ điều khiển.
- Đọc SV và trạng thái alarm nếu register map hỗ trợ.
- Dashboard thời gian thực.
- High Alarm, Low Alarm, sensor error và communication error.
- Âm thanh cảnh báo trên máy tính.
- Xác nhận alarm bởi người vận hành.
- Lưu lịch sử nhiệt độ, cảnh báo và lỗi kết nối.
- Biểu đồ nhiệt độ thời gian thực và lịch sử.
- Tra cứu, lọc và xuất CSV hoặc Excel.
- Quản lý cấu hình tank, thiết bị và ngưỡng.
- Phân quyền cơ bản và nhật ký thao tác.
- Sao lưu, khôi phục dữ liệu.
- Tự động khởi động cùng Windows và tự kết nối lại.
- Bộ cài đặt, tài liệu hướng dẫn và bàn giao mã nguồn.

### 4.2 Chức năng chưa nằm trong phiên bản 1

Các chức năng sau chỉ thực hiện khi có yêu cầu bổ sung:

- Giám sát từ xa qua Internet.
- Web application hoặc mobile application.
- Ghi hoặc thay đổi SV từ máy tính.
- Điều khiển thiết bị hoặc liên động công nghệ từ phần mềm.
- Gửi email, SMS, Zalo, Teams hoặc thông báo điện thoại.
- Kết nối với MES, SCADA trung tâm hoặc hệ thống quản lý của Samsung.
- High availability, máy chủ dự phòng hoặc đồng bộ nhiều máy trạm.
- Chữ ký điện tử và quy trình phê duyệt nhiều cấp.

## 5. Thông tin thiết bị và truyền thông

### 5.1 Bộ điều khiển

- Dòng thiết bị: Autonics TK Series, dự kiến TK4W.
- Giao thức: Modbus RTU trên RS485.
- RS485 dạng hai dây half-duplex.
- Với TK4W, tài liệu thể hiện chân 18 là RS485 A+ và chân 19 là RS485 B-.
- Thiết bị phải thuộc biến thể có RS485, ví dụ tùy chọn T hoặc B.
- Model TK4W-R4RN trong phương án ban đầu chỉ có transmission output 4-20 mA, không phù hợp với driver RS485 nếu không bổ sung phần cứng chuyển đổi.

### 5.2 Tham số truyền thông mặc định

| Tham số | Giá trị ban đầu đề xuất |
|---|---:|
| Protocol | Modbus RTU |
| Baud rate | 9.600 bps |
| Data bits | 8 |
| Parity | None |
| Stop bits | 2 |
| Response waiting time | Khoảng 20 ms |
| Device address | Duy nhất cho từng controller |
| Timeout phần mềm | 500–1.000 ms, có thể cấu hình |
| Retry | 2–3 lần, có thể cấu hình |

Thông số thực tế phải được kiểm tra trên từng controller trước khi chạy chính thức.

### 5.3 Số lượng thiết bị trên tuyến

- Một tuyến TK Series hỗ trợ tối đa 31 thiết bị theo tài liệu Autonics.
- Giai đoạn 21 tank có thể dùng một tuyến nếu chất lượng cáp và thời gian quét đáp ứng.
- Khi mở rộng lên 40 tank, phần mềm phải hỗ trợ ít nhất hai COM port hoặc hai gateway RS485.
- Mỗi tuyến phải được cấu hình và vận hành độc lập. Lỗi trên một tuyến không được làm dừng tuyến còn lại.

### 5.4 Dữ liệu cần đọc từ thiết bị

Tối thiểu cần xác định được các thanh ghi sau:

- Present Value, PV.
- Set Value, SV.
- Trạng thái High Alarm.
- Trạng thái Low Alarm.
- Sensor break hoặc sensor error.
- Trạng thái RUN/STOP nếu thiết bị cung cấp.
- Đơn vị và vị trí dấu thập phân.

Các địa chỉ thanh ghi, function code, scale và kiểu dữ liệu phải lấy từ tài liệu truyền thông chính thức. Không được suy đoán register khi phát hành phiên bản vận hành.

## 6. Kiến trúc phần mềm đề xuất

```text
Windows Forms UI
       |
Application Services
       |-- Device polling service
       |-- Alarm service
       |-- Historian service
       |-- Reporting service
       |-- Configuration service
       |
Autonics Modbus RTU driver
       |
COM1 / COM2 / Gateway RS485
       |
Autonics TK4W controllers

Application Services
       |
SQLite database
```

### 6.1 Các lớp chính

- Presentation: form, user control, dashboard, chart và dialog cấu hình.
- Application: điều phối polling, alarm, tra cứu, báo cáo và backup.
- Domain: Tank, Device, TemperatureSample, AlarmEvent và các quy tắc nghiệp vụ.
- Infrastructure: SerialPort, Modbus RTU, database, logging và file export.
- Background services: đọc thiết bị và lưu dữ liệu mà không khóa giao diện.

### 6.2 Công nghệ đề xuất

- C# và Windows Forms.
- .NET 10 LTS, target `net10.0-windows`, build x64.
- SQLite cho hệ thống một máy trạm.
- SQL Server chỉ dùng khi có yêu cầu nhiều máy cùng truy cập hoặc lưu trữ tập trung.
- Giao tiếp serial thông qua `System.IO.Ports` và lớp Modbus RTU riêng hoặc thư viện đã được kiểm tra.
- Logging dạng file có giới hạn kích thước và tự xoay vòng.
- Installer dạng MSI hoặc bộ cài tương đương đã được bộ phận IT chấp thuận.

## 7. Yêu cầu chức năng

### FR-01 Quản lý tank

- Thêm, sửa, kích hoạt và vô hiệu hóa tank.
- Mỗi tank có mã duy nhất, tên hiển thị, vị trí, loại hóa chất và mô tả.
- Mỗi tank liên kết với một tuyến truyền thông và một địa chỉ Modbus.
- Không cho phép trùng địa chỉ trên cùng một tuyến.

### FR-02 Quản lý kết nối

- Cấu hình COM port, baud rate, parity, stop bits, timeout và retry.
- Có nút kiểm tra kết nối.
- Hiển thị trạng thái của từng tuyến và từng thiết bị.
- Tự kết nối lại sau khi mất COM port, gateway hoặc controller.
- Cho phép dừng và khởi động lại một tuyến mà không đóng ứng dụng.

### FR-03 Thu thập dữ liệu

- Đọc lần lượt từng thiết bị, không gửi nhiều request đồng thời trên cùng một tuyến RS485.
- Chu kỳ quét mặc định 2 giây, cấu hình được trong khoảng 1–10 giây.
- Giao diện không được treo khi thiết bị timeout.
- Dữ liệu đọc được phải có timestamp và trạng thái chất lượng.
- Không ghi dữ liệu giả hoặc giá trị cũ thành dữ liệu mới khi thiết bị mất kết nối.

### FR-04 Dashboard

- Hiển thị đồng thời 21 tank trên màn hình 24–27 inch, độ phân giải mục tiêu 1920 x 1080.
- Hỗ trợ phân trang hoặc nhóm khu vực khi mở rộng lên 40 tank.
- Mỗi tank hiển thị tối thiểu:
  - Mã và tên tank.
  - Loại hóa chất.
  - PV.
  - SV nếu đọc được.
  - Ngưỡng Low và High.
  - Thời gian cập nhật gần nhất.
  - Trạng thái kết nối.
  - Trạng thái alarm.
- Màu trạng thái đề xuất:
  - Xanh lá: bình thường.
  - Đỏ: High Alarm.
  - Cam hoặc xanh dương: Low Alarm.
  - Tím: sensor error.
  - Xám: mất kết nối hoặc chưa có dữ liệu.
- Có chế độ toàn màn hình.

### FR-05 Alarm engine

- Phát hiện High Alarm và Low Alarm theo từng tank.
- Phát hiện sensor error dựa trên thanh ghi trạng thái hoặc giá trị không hợp lệ.
- Phát hiện mất kết nối sau số lần đọc lỗi liên tiếp có thể cấu hình.
- Có hysteresis để tránh alarm bật tắt liên tục.
- Có thời gian trễ ON và OFF nếu người quản trị cấu hình.
- Xác nhận alarm không đồng nghĩa với xóa điều kiện alarm.
- Alarm chỉ kết thúc khi điều kiện thực tế trở lại bình thường.
- Khi alarm xuất hiện phải:
  - Đổi màu tank.
  - Hiển thị banner hoặc danh sách alarm đang hoạt động.
  - Phát âm thanh trên máy tính.
  - Ghi lịch sử ngay lập tức.
- Khi kết nối được khôi phục phải ghi sự kiện Communication Restored.

### FR-06 Lịch sử alarm

- Lưu thời gian bắt đầu và kết thúc.
- Lưu tank, loại alarm, PV, SV, ngưỡng Low/High và trạng thái kết nối.
- Lưu người xác nhận và thời gian xác nhận.
- Cho phép thêm ghi chú vận hành.
- Cho phép lọc theo thời gian, tank, hóa chất, loại alarm và trạng thái xác nhận.

### FR-07 Lưu lịch sử nhiệt độ

- Chu kỳ lưu mặc định 60 giây, cấu hình được.
- Lưu ngay một mẫu khi có alarm, thay đổi trạng thái hoặc phục hồi kết nối.
- Không lưu lặp vô hạn dữ liệu lỗi khi thiết bị mất kết nối.
- Chính sách lưu dữ liệu phải cấu hình được; đề xuất ban đầu tối thiểu 12 tháng.
- Có công cụ dọn dữ liệu cũ theo chính sách đã được phê duyệt.

### FR-08 Biểu đồ và tra cứu

- Hiển thị trend của một hoặc nhiều tank.
- Chọn khoảng thời gian tùy ý.
- Hiển thị đường High/Low limit trên biểu đồ.
- Cho phép phóng to, thu nhỏ và xem giá trị tại từng thời điểm.
- Dữ liệu phải phân biệt giá trị tốt, mất kết nối và sensor error.

### FR-09 Xuất dữ liệu

- Xuất CSV hoặc Excel.
- Hỗ trợ xuất lịch sử nhiệt độ và lịch sử alarm.
- File xuất phải có tên tank, thời gian, PV, SV, ngưỡng và trạng thái.
- Định dạng ngày giờ phải thống nhất theo múi giờ vận hành tại Việt Nam.

### FR-10 Người dùng và phân quyền

Tối thiểu có hai vai trò:

- Operator:
  - Xem dashboard và lịch sử.
  - Xác nhận alarm.
  - Xuất báo cáo.
- Administrator:
  - Có toàn bộ quyền Operator.
  - Thay đổi cấu hình thiết bị, tank, ngưỡng và hệ thống.
  - Backup và restore.

Nếu chưa triển khai tài khoản người dùng trong phiên bản đầu, việc thay đổi cấu hình phải được bảo vệ bằng mật khẩu quản trị.

### FR-11 Nhật ký thao tác

- Ghi nhận đăng nhập và đăng xuất.
- Ghi thay đổi cấu hình tank, địa chỉ Modbus và thông số COM port.
- Ghi thay đổi ngưỡng High/Low.
- Ghi thao tác xác nhận alarm.
- Nếu cho phép ghi SV, phải lưu giá trị trước, giá trị sau, người thao tác và thời gian.

### FR-12 Backup và restore

- Backup database thủ công.
- Hỗ trợ backup tự động theo lịch nếu được bật.
- Kiểm tra tính hợp lệ của file backup trước khi restore.
- Không cho restore khi hệ thống đang ghi dữ liệu mà chưa dừng an toàn.
- Có hướng dẫn phục hồi khi thay máy tính.

### FR-13 Chẩn đoán và log

- Ghi lỗi mở COM port, timeout, CRC, Modbus exception và lỗi database.
- Không ghi mật khẩu dưới dạng rõ trong log.
- Có màn hình chẩn đoán cho quản trị viên.
- Cho phép xem request/response Modbus ở chế độ kỹ thuật; mặc định phải tắt để tránh file log quá lớn.

## 8. Quy tắc nhiệt độ và cảnh báo ban đầu

| Hóa chất | Giá trị danh định | Low đề xuất | High đề xuất |
|---|---:|---:|---:|
| H2SO4 | 35°C ± 5°C | 30°C | 40°C |
| H2O2 | 25°C ± 5°C | 20°C | 30°C |

Các giá trị trên lấy từ phương án ban đầu và phải được chủ quản công nghệ hoặc EHS xác nhận. Phần mềm phải cho phép thiết lập riêng từng tank vì ngưỡng thực tế có thể khác nhau.

Thiết lập alarm đề xuất ban đầu:

- Hysteresis: 0,5–1,0°C, cấu hình được.
- Delay ON: 5–10 giây, cấu hình được.
- Delay OFF: 5–10 giây, cấu hình được.
- Mất kết nối: sau 3 lần polling thất bại liên tiếp.
- Âm thanh alarm có thể tắt tạm thời sau khi người vận hành xác nhận nhưng trạng thái hình ảnh vẫn duy trì.

## 9. Mô hình dữ liệu tối thiểu

### Tanks

- Id.
- Code.
- Name.
- ChemicalType.
- Location.
- DeviceId.
- LowLimit.
- HighLimit.
- Hysteresis.
- AlarmDelay.
- Enabled.

### Devices

- Id.
- ConnectionId.
- ModbusAddress.
- Model.
- SerialNumber nếu có.
- PollingInterval.
- Enabled.
- LastCommunicationTime.

### Connections

- Id.
- Name.
- PortName.
- BaudRate.
- DataBits.
- Parity.
- StopBits.
- Timeout.
- RetryCount.

### TemperatureSamples

- Id.
- TankId.
- Timestamp.
- PV.
- SV.
- Quality.
- DeviceStatus.

### AlarmEvents

- Id.
- TankId.
- AlarmType.
- StartTime.
- EndTime.
- StartValue.
- EndValue.
- LowLimit.
- HighLimit.
- AcknowledgedBy.
- AcknowledgedAt.
- Note.

### AuditLogs

- Id.
- Timestamp.
- UserName.
- Action.
- ObjectType.
- ObjectId.
- OldValue.
- NewValue.

## 10. Yêu cầu phi chức năng

### NFR-01 Hiệu năng

- UI phải phản hồi bình thường trong khi polling thiết bị.
- Thời gian hiển thị giá trị mới không lớn hơn một chu kỳ polling cộng thời gian đọc toàn tuyến.
- Tra cứu dữ liệu một tháng của một tank phải hoàn thành trong thời gian mục tiêu dưới 3 giây trên máy vận hành tiêu chuẩn.
- Việc xuất dữ liệu lớn phải chạy nền và có thông báo tiến độ.

### NFR-02 Độ ổn định

- Không crash khi mất COM port, rút USB-RS485, controller mất nguồn hoặc database tạm thời bị khóa.
- Ứng dụng phải tự phục hồi kết nối khi điều kiện bình thường trở lại.
- Một thiết bị lỗi không được chặn polling các thiết bị khác.
- Một tuyến RS485 lỗi không được làm dừng tuyến khác.

### NFR-03 An toàn dữ liệu

- Giao dịch database phải bảo đảm không tạo bản ghi alarm dở dang.
- Backup phải được kiểm tra có thể mở lại.
- Không xóa dữ liệu vận hành nếu chưa có xác nhận và chính sách lưu trữ.
- Đồng hồ máy tính phải được đồng bộ thời gian theo chính sách IT của nhà máy.

### NFR-04 Bảo mật

- Chỉ Administrator được thay đổi cấu hình quan trọng.
- Mật khẩu phải lưu bằng phương pháp băm, không lưu dạng rõ.
- Không mở cổng mạng khi phiên bản 1 chỉ hoạt động nội bộ trên một máy.
- Nếu bổ sung truy cập từ xa, phải dùng mạng nội bộ hoặc VPN và được IT Samsung phê duyệt.

### NFR-05 Khả năng bảo trì

- Mã nguồn phải chia module và không đặt logic truyền thông trực tiếp trong Form.
- Thông tin register phải tách khỏi giao diện, có thể thay đổi theo model hoặc firmware.
- Có unit test cho CRC16, parse register, alarm state machine và lưu dữ liệu.
- Có simulator để phát triển và kiểm thử khi không có thiết bị thật.

### NFR-06 Triển khai

- Chạy trên Windows 10/11 hoặc phiên bản Windows được IT Samsung phê duyệt.
- Hỗ trợ độ phân giải 1920 x 1080 và DPI scaling thông dụng.
- Có bộ cài đặt và quy trình nâng cấp không làm mất database/configuration.
- Có tùy chọn tự khởi động cùng Windows.

## 11. Các màn hình dự kiến

1. Đăng nhập.
2. Dashboard tổng thể.
3. Chi tiết tank và trend thời gian thực.
4. Danh sách alarm đang hoạt động.
5. Lịch sử alarm.
6. Lịch sử nhiệt độ và biểu đồ.
7. Cấu hình tank.
8. Cấu hình kết nối RS485.
9. Cấu hình ngưỡng và lưu dữ liệu.
10. Quản lý người dùng.
11. Backup và restore.
12. Nhật ký hệ thống và chẩn đoán truyền thông.

## 12. Điều kiện đầu vào để bắt đầu phát triển

Các điều kiện sau cần có hoặc cần được xác nhận:

- Chốt số lượng phiên bản 1 là 21 tank.
- Danh sách mã tank, vị trí và loại hóa chất.
- Xác nhận model controller thực tế có RS485.
- Xác nhận một tuyến hay nhiều tuyến RS485.
- Danh sách địa chỉ Modbus của từng controller.
- Communication user manual hoặc register map chính thức.
- Một controller TK4W RS485 thật.
- Một bộ chuyển đổi USB-RS485 hoặc gateway sử dụng thực tế.
- Xác nhận ngưỡng High/Low của từng tank.
- Xác nhận có cho phép phần mềm ghi SV hay chỉ đọc.
- Xác nhận chu kỳ lưu và thời gian lưu dữ liệu.
- Xác nhận định dạng báo cáo cần xuất.
- Xác nhận phiên bản Windows, chính sách cài đặt và quyền Administrator trên máy vận hành.
- Xác nhận yêu cầu tài khoản người dùng và phân quyền.

Nếu chưa có thiết bị thật, có thể phát triển với simulator nhưng không được nghiệm thu chức năng truyền thông cho đến khi kiểm thử trên phần cứng thực tế.

## 13. Điều kiện hoàn thành phần mềm

Phần mềm chỉ được coi là hoàn thành khi đáp ứng đầy đủ các điều kiện sau:

### 13.1 Hoàn thành chức năng

- Tất cả chức năng trong phạm vi phiên bản 1 đã được triển khai.
- Không còn chức năng bắt buộc ở trạng thái placeholder hoặc hard-code không được phê duyệt.
- Dashboard hiển thị đúng danh sách tank và trạng thái.
- Tra cứu, biểu đồ, alarm, export, backup và restore hoạt động đúng.

### 13.2 Hoàn thành truyền thông

- Đọc đúng PV từ controller thật.
- Đọc đúng SV và trạng thái nếu nằm trong phạm vi register map.
- Scale số thập phân và đơn vị đúng với màn hình controller.
- Kiểm thử được timeout, CRC error, mất nguồn, mất cáp và khôi phục kết nối.
- Không trùng địa chỉ thiết bị.
- Polling đủ số lượng thiết bị trong chu kỳ đã thống nhất.

### 13.3 Hoàn thành alarm

- Mô phỏng và kiểm thử High, Low, sensor error và communication error.
- Alarm được tạo một lần khi bắt đầu, không tạo bản ghi lặp ở mỗi chu kỳ polling.
- Ghi đúng thời gian bắt đầu, kết thúc và xác nhận.
- Hysteresis và delay hoạt động đúng.
- Sau khi khởi động lại ứng dụng, trạng thái alarm phải được khôi phục hợp lý từ điều kiện thực tế và database.

### 13.4 Hoàn thành dữ liệu

- Lưu dữ liệu liên tục đúng chu kỳ.
- Không lưu giá trị lỗi như giá trị nhiệt độ hợp lệ.
- Tra cứu và xuất dữ liệu cho kết quả đúng.
- Backup tạo thành công và restore được trên môi trường kiểm thử.
- Việc nâng cấp phiên bản không làm mất dữ liệu đã có.

### 13.5 Hoàn thành kiểm thử

- Unit test quan trọng chạy thành công.
- Integration test với ít nhất một controller thật hoàn thành.
- Test nhiều địa chỉ bằng thiết bị thật hoặc simulator hoàn thành.
- Chạy liên tục tối thiểu 72 giờ mà không có crash hoặc mất dữ liệu nghiêm trọng.
- UAT được người đại diện vận hành xác nhận.

### 13.6 Hoàn thành bàn giao

- Bàn giao mã nguồn và file solution.
- Bàn giao bản release và installer.
- Bàn giao cấu hình mẫu.
- Bàn giao database schema và hướng dẫn backup/restore.
- Bàn giao tài liệu cài đặt, vận hành và xử lý sự cố cơ bản.
- Bàn giao danh sách dependency và license liên quan.
- Đào tạo người vận hành và quản trị viên.

## 14. Kịch bản nghiệm thu chính

| Mã | Kịch bản | Kết quả mong đợi |
|---|---|---|
| AT-01 | Kết nối controller hợp lệ | Hiển thị Online và đọc đúng PV |
| AT-02 | Rút cáp RS485 | Thiết bị chuyển Offline sau số lần retry quy định và tạo alarm |
| AT-03 | Cắm lại cáp | Tự kết nối lại và ghi Communication Restored |
| AT-04 | PV vượt High | Tank chuyển trạng thái High, phát âm thanh và ghi lịch sử |
| AT-05 | PV thấp hơn Low | Tank chuyển trạng thái Low và ghi lịch sử |
| AT-06 | PV trở lại bình thường | Alarm kết thúc sau hysteresis/delay đã cấu hình |
| AT-07 | Sensor bị ngắt | Hiển thị Sensor Error, không coi giá trị lỗi là nhiệt độ hợp lệ |
| AT-08 | Xác nhận alarm | Lưu đúng người và thời gian xác nhận, alarm vẫn còn nếu điều kiện chưa hết |
| AT-09 | Khởi động lại ứng dụng | Tự mở kết nối, tiếp tục polling và giữ dữ liệu cũ |
| AT-10 | Tra cứu lịch sử | Trả đúng dữ liệu theo tank và khoảng thời gian |
| AT-11 | Xuất báo cáo | File xuất mở được và nội dung khớp dữ liệu tra cứu |
| AT-12 | Backup và restore | Khôi phục được cấu hình và lịch sử trên máy kiểm thử |
| AT-13 | Một thiết bị timeout | Các thiết bị còn lại vẫn được cập nhật |
| AT-14 | Một tuyến RS485 lỗi | Tuyến còn lại vẫn hoạt động |
| AT-15 | Chạy liên tục 72 giờ | Không crash, không treo UI và không mất dữ liệu nghiêm trọng |

## 15. Sản phẩm bàn giao

- Source code C# WinForms.
- File solution và project.
- Script hoặc migration tạo database.
- Bộ cài đặt bản Release x64.
- File cấu hình mẫu cho 21 tank.
- Simulator hoặc công cụ test Modbus dùng trong phát triển.
- Test cases và kết quả kiểm thử.
- Tài liệu cài đặt.
- Tài liệu hướng dẫn vận hành.
- Tài liệu cấu hình thiết bị và địa chỉ Modbus.
- Tài liệu backup, restore và xử lý sự cố.
- Danh sách phiên bản, dependency và giấy phép sử dụng.

## 16. Kế hoạch thực hiện khi sử dụng AI hỗ trợ

Giả định một lập trình viên WinForms có kinh nghiệm, làm việc toàn thời gian và sử dụng AI để hỗ trợ tạo cấu trúc, UI, database, test và tài liệu.

| Giai đoạn | Công việc | Thời gian dự kiến |
|---|---|---:|
| 1 | Chốt yêu cầu, register map và wireframe | 2–3 ngày |
| 2 | Tạo solution, kiến trúc, database và configuration | 1–2 ngày |
| 3 | Driver Modbus RTU, simulator, retry và reconnect | 2–3 ngày |
| 4 | Dashboard 21/40 tank | 2–3 ngày |
| 5 | Alarm engine và lịch sử alarm | 2–3 ngày |
| 6 | Historian, trend, tra cứu và export | 2–3 ngày |
| 7 | Phân quyền, audit log, backup và installer | 2–3 ngày |
| 8 | Kiểm thử controller thật, UAT và sửa lỗi | 3–5 ngày |

Ước lượng tổng:

- MVP: 7–10 ngày làm việc.
- Bản vận hành đầy đủ: 15–20 ngày làm việc, tương đương khoảng 3–4 tuần.
- Nếu thiếu register map hoặc chưa có thiết bị thật: cộng thêm 3–7 ngày làm việc.
- Nếu bổ sung ghi SV hoặc giám sát từ xa: cộng thêm khoảng 1–2 tuần tùy yêu cầu bảo mật và phê duyệt IT.

AI giúp rút ngắn việc tạo mã nguồn, UI, database, test và tài liệu. AI không thay thế được kiểm thử RS485, xác nhận register map, kiểm tra tín hiệu thực tế và UAT với người vận hành.

## 17. Rủi ro project

| Rủi ro | Mức ảnh hưởng | Biện pháp |
|---|---|---|
| Controller thực tế là R4RN không có RS485 | Cao | Xác nhận model trước khi lập trình driver hoặc bổ sung kiến trúc analog/gateway |
| Thiếu register map Modbus | Cao | Lấy communication manual và sample C# chính thức |
| Không có thiết bị thật để kiểm thử | Cao | Dùng simulator trong phát triển, bắt buộc test thật trước nghiệm thu |
| Trùng địa chỉ Modbus | Cao | Kiểm tra cấu hình và validate trong phần mềm |
| Nhiễu hoặc timeout RS485 | Cao | Retry, reconnect, log lỗi và phối hợp kiểm tra hệ thống cáp |
| Phạm vi thay đổi từ 21 lên 40 tank | Trung bình | Thiết kế multi-connection ngay từ đầu |
| Ngưỡng alarm chưa được phê duyệt | Cao | Chủ quản công nghệ/EHS xác nhận trước UAT |
| Yêu cầu remote access phát sinh muộn | Trung bình | Tách thành giai đoạn 2 và thực hiện đánh giá bảo mật |
| Database tăng nhanh | Trung bình | Cấu hình chu kỳ lưu, retention, index và backup |
| Máy tính bị tắt hoặc Windows Update | Trung bình | Auto-start, giám sát ứng dụng và thống nhất chính sách IT |
| Thay đổi SV không được kiểm soát | Cao | Phiên bản đầu read-only; nếu mở ghi phải có phân quyền và audit |

## 18. Các quyết định cần xác nhận

- [ ] Phạm vi chính thức là 21 tank hay 40 tank.
- [ ] Model controller chính xác của từng tank.
- [ ] Controller có RS485 hay chỉ có 4-20 mA.
- [ ] Có bao nhiêu tuyến RS485 và COM port/gateway.
- [ ] Register map chính thức đã được cung cấp.
- [ ] Có cho phép ghi SV từ phần mềm.
- [ ] Ngưỡng High/Low của từng tank đã được phê duyệt.
- [ ] Chu kỳ polling.
- [ ] Chu kỳ lưu và thời hạn lưu dữ liệu.
- [ ] Yêu cầu file báo cáo.
- [ ] Có cần tài khoản người dùng hay chỉ mật khẩu quản trị.
- [ ] Có cần chạy toàn màn hình và khóa thao tác ngoài ứng dụng.
- [ ] Có cần giám sát từ xa trong giai đoạn sau.
- [ ] Máy tính, Windows và chính sách cài đặt đã được IT xác nhận.
- [ ] Người đại diện nghiệm thu và quy trình UAT đã được xác định.

## 19. Nguyên tắc quản lý thay đổi

- Yêu cầu mới sau khi chốt phạm vi phải được ghi thành change request.
- Change request phải mô tả mục tiêu, mức ưu tiên, tác động đến dữ liệu, UI, thiết bị và bảo mật.
- Các nội dung như remote access, ghi SV, tích hợp hệ thống ngoài hoặc thay đổi database trung tâm được xem là thay đổi phạm vi đáng kể.
- Tiến độ chỉ được chốt chính thức sau khi hoàn thành các mục trong phần Điều kiện đầu vào.

## 20. Kết luận

Phiên bản 1 nên tập trung vào giám sát ổn định tại chỗ: đọc 21 controller RS485, dashboard, cảnh báo, lịch sử, biểu đồ, tra cứu, export, backup và chẩn đoán kết nối. Kiến trúc phải hỗ trợ tối thiểu 40 tank và nhiều tuyến RS485 ngay từ đầu.

Điều kiện quan trọng nhất để đảm bảo tiến độ 3–4 tuần là có register map chính thức, controller RS485 thật, bộ chuyển đổi sử dụng thực tế và yêu cầu alarm được phê duyệt trước khi bắt đầu kiểm thử tích hợp.
