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
| Cơ sở dữ liệu | PostgreSQL |
| Ngày lập tài liệu | 07/10/2026 |
| Ngày cập nhật | 08/10/2026 |
| Trạng thái | Bản yêu cầu ban đầu, cần xác nhận trước khi phát triển |

## 2. Tài liệu đầu vào

Project được xây dựng dựa trên các tài liệu sau:

1. `Phương án thi công giám sát hóa chất SEMV.docx` — tài liệu nội bộ, lưu cục bộ trong `docs-private/`, không phát hành trong Git.
2. `High-performance-PID-temperature-controllers-AUTONICS-TK-series-MANUAL-228.pdf` — tài liệu tham khảo cục bộ trong `docs-private/`.
3. `TK_C#_Sample_ReadMe_ENG.xlsx` — tài liệu mẫu cục bộ trong `docs-private/`.
4. Hướng dẫn TK Series của Autonics: https://www.autonics.com/glb/data/manual/en/TK
5. Chương trình mẫu C# cho TK Series của Autonics: https://www.autonics.com/us/service/data/view/9/247455

Các tài liệu riêng tư không được tạo liên kết tương đối từ tài liệu public vì người clone repository sẽ không có chúng. Khi cần kiểm chứng, thành viên được cấp quyền phải lấy bản nội bộ từ kênh chia sẻ do chủ project quản lý.

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
- Tra cứu, lọc và xuất CSV. Excel là P2 mặc định; chỉ nâng thành P1 tại M0 khi thư viện, giấy phép và phạm vi đã được IT/pháp chế/Product Owner chấp thuận.
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
- Tách acquisition thành Windows Service độc lập nằm ngoài baseline; có thể được nâng vào V1 tại M0 cùng điều chỉnh thêm 3–5 ngày.

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
- Thiết bị đang Online được polling theo chu kỳ cấu hình. Sau khi đã xác định Offline, mỗi lần probe chỉ gửi một request, không retry; probe chạy theo backoff 10–30 giây, ưu tiên thấp và tổng thời gian probe không vượt quá 10–15% ngân sách chu kỳ bus.

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

Chương trình mẫu Autonics hiện có chỉ được dùng làm bằng chứng tham khảo cho một phần register map. Các điểm đang thấy trong sample gồm Model `0x0068`, PV `0x03E8`, SV `0x03EB` và Status `0x03EE`; các địa chỉ này vẫn phải đối chiếu communication manual và controller thật trước khi khóa cấu hình production.

Trước khi hoàn thành driver phải lập bảng register có kiểm soát phiên bản, tối thiểu gồm: model/firmware áp dụng, function code, địa chỉ, số register, quyền read/write, kiểu signed/unsigned, byte/word order, scale, đơn vị, bit mask, giá trị sensor error và nguồn tài liệu. PV/SV phải được kiểm thử với giá trị âm bằng `Int16` hoặc kiểu signed đúng theo manual, sau đó mới áp dụng scale/dấu thập phân.

Phải thực hiện spike đo hai phương án:

- Đọc từng register riêng lẻ.
- Đọc block liên tục từ `0x03E8` đến `0x03EE` nếu manual và controller cho phép đọc các khoảng giữa.

Chỉ dùng block read khi controller thật trả dữ liệu ổn định, parser ánh xạ đúng và thời gian quét tốt hơn. Nếu không đạt, driver phải tự dùng danh sách request riêng lẻ đã được xác nhận. Không được giả định các register nằm giữa là hợp lệ.

### 5.5 Ngân sách thời gian polling

Ở cấu hình 9.600 bps, 8N2, mỗi byte serial chiếm 11 bit. Ước lượng ban đầu để thiết kế spike:

- Block read 7 register: request 8 byte, response khoảng 19 byte, response waiting khoảng 20 ms và silent interval 3,5 character, tổng xấp xỉ 55 ms/thiết bị; 21 thiết bị khoảng 1,15 giây trong điều kiện lý tưởng.
- Ba single-register read: xấp xỉ 41 ms/request × 3 × 21, khoảng 2,6 giây lý tưởng, chưa tính jitter, xử lý phần mềm, timeout hoặc retry.

Do đó chu kỳ 2 giây chỉ được cam kết khi block read được controller thật xác nhận và benchmark còn đủ dự phòng. Nếu phải đọc riêng lẻ, phần mềm phải hỗ trợ lịch nhiều tần suất, ví dụ PV 2–3 giây và SV/status 5–10 giây, hoặc tăng chu kỳ toàn tuyến lên 3–5 giây.

Spike phải ghi lại kích thước frame thực tế, thời gian truyền request/response, silent interval, response waiting, thời gian xử lý, retry/probe budget, p50/p95/p99 của chu kỳ 21 và 40 thiết bị. Chu kỳ production được chốt từ số đo, không chỉ từ phép tính lý thuyết.

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
PostgreSQL database server
```

### 6.1 Các lớp chính

- Presentation: form, user control, dashboard, chart và dialog cấu hình.
- Application: điều phối polling, alarm, tra cứu, báo cáo, migration và backup.
- Domain: Tank, Device, TemperatureSample, AlarmEvent và các quy tắc nghiệp vụ.
- Infrastructure: SerialPort, Modbus RTU, database, logging và file export.
- Background services: đọc thiết bị và lưu dữ liệu mà không khóa giao diện.

### 6.2 Công nghệ đề xuất

- C# và Windows Forms.
- .NET 10 LTS, target `net10.0-windows`, build x64.
- PostgreSQL là cơ sở dữ liệu chính của hệ thống.
- Phiên bản PostgreSQL do IT phê duyệt; đề xuất dùng phiên bản còn được cộng đồng hỗ trợ tại thời điểm triển khai, tối thiểu PostgreSQL 16.
- Kết nối từ C# qua `Npgsql`; truy cập dữ liệu bằng Dapper hoặc lớp repository tương đương, không viết SQL trực tiếp trong Form.
- Schema phải được quản lý bằng migration có đánh số phiên bản và có khả năng kiểm tra phiên bản database khi ứng dụng khởi động.
- Mặc định phiên bản 1 có thể chạy PostgreSQL như Windows Service trên máy giám sát. Nếu dùng máy chủ database tập trung, địa chỉ máy chủ, firewall, TLS, tài khoản dịch vụ và chính sách backup phải được IT phê duyệt trước triển khai.
- Giao tiếp serial thông qua `System.IO.Ports` và lớp Modbus RTU riêng hoặc thư viện đã được kiểm tra.
- Logging dạng file có giới hạn kích thước và tự xoay vòng.
- Installer dạng MSI hoặc bộ cài tương đương đã được bộ phận IT chấp thuận.
- Thư viện chart phải được chọn và khóa version tại M0 sau một spike kiểm tra: license, hỗ trợ .NET/WinForms mục tiêu, DPI, cập nhật realtime, downsampling dữ liệu dài hạn, mức dùng CPU/RAM và khả năng export. Chưa chọn được chart library thì hạng mục UI trend ở trạng thái Blocked.

### 6.3 Quyết định tiến trình và watchdog phiên bản 1

- Trước ngày phát triển đầu tiên, M0 phải khóa một trong hai phương án: (A) WinForms background task + Task Scheduler/watchdog, hoặc (B) Windows Service acquisition + WinForms UI. Không để quyết định kiến trúc này đến tuần hardening.
- Baseline của 25 ngày giữ acquisition, alarm và historian trong cùng ứng dụng WinForms nhưng chạy bằng background task/service nội bộ, không đặt logic trong Form và không khóa UI.
- Ứng dụng được khởi động bằng Windows Task Scheduler ở chế độ `At startup/At logon` theo chính sách IT; watchdog kiểm tra tiến trình, ghi sự kiện và khởi động lại khi ứng dụng dừng ngoài kế hoạch.
- Ứng dụng phải có heartbeat nội bộ cho polling, historian và database queue; trạng thái `Healthy/Degraded/Faulted` hiển thị trên màn hình chẩn đoán.
- Việc restart tự động phải có giới hạn và backoff để tránh vòng lặp khởi động liên tục; mọi lần restart phải được log.
- Nếu nhà máy yêu cầu vẫn thu thập dữ liệu khi chưa đăng nhập Windows hoặc tách quyền UI/acquisition, phải chuyển acquisition sang Windows Service. Đây là thay đổi kiến trúc ngoài phạm vi phiên bản 1 và ước lượng thêm 3–5 ngày phát triển cùng kiểm thử cài đặt/nâng cấp.

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
- Driver ưu tiên block read đã được xác nhận trên controller thật; nếu không được hỗ trợ phải fallback sang các request riêng lẻ mà không thay đổi dữ liệu đầu ra.
- Thiết bị Online và Offline phải có lịch polling riêng. Sau khi thiết bị được xác định Offline, probe lại theo backoff cấu hình được từ 10–30 giây, một request không retry; một thiết bị Offline không được chiếm toàn bộ thời gian quét của tuyến.
- Scheduler phải có time budget cho từng chu kỳ, ưu tiên PV của thiết bị Online; SV/status và offline probe được chạy ở tần suất thấp hơn hoặc dời sang chu kỳ sau khi gần hết ngân sách.
- Mỗi request chỉ retry số lần giới hạn; không retry vô hạn và không tạo nhiều request đang chờ trên cùng một tuyến.
- Giao diện không được treo khi thiết bị timeout.
- Dữ liệu đọc được phải có timestamp và trạng thái chất lượng.
- Không ghi dữ liệu giả hoặc giá trị cũ thành dữ liệu mới khi thiết bị mất kết nối.
- Parser phải xử lý đúng signed/unsigned, scale, decimal position và giá trị sensor error theo register map được phê duyệt.

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
- Phải có alarm cấp `Connection/Bus` để gộp sự cố COM port/gateway hoặc lỗi đồng thời toàn tuyến. Khi bus alarm hoạt động, tank vẫn hiển thị Offline nhưng không phát 21 âm thanh/sự kiện độc lập; các trạng thái con phải có `CorrelationId/RootCauseAlarmId` hoặc được suppress theo quy tắc đã phê duyệt.
- Mỗi trạng thái cảnh báo phải lưu rõ nguồn:
  - `SoftwareAlarm`: phần mềm tính từ PV, ngưỡng, hysteresis và delay.
  - `ControllerAlarm`: bit/trạng thái đọc trực tiếp từ bộ điều khiển.
  - `LocalHardwareAlarm`: dành cho P2, chỉ bật khi có DI/gateway và register/tín hiệu đầu vào đã được phê duyệt.
- Alarm tại controller là lớp bảo vệ cục bộ; SoftwareAlarm phục vụ dashboard, lịch sử và hỗ trợ vận hành, không thay thế liên động an toàn.
- Không được mặc định AL1 là High và AL2 là Low nếu chưa xác nhận cấu hình alarm của controller.
- Controller alarm threshold và SoftwareAlarm threshold là hai cấu hình độc lập trong V1 read-only. Nếu controller cho phép đọc alarm setpoint, phần mềm phải hiển thị riêng giá trị controller và phần mềm. Nếu không đọc được, việc đối chiếu controller threshold được thực hiện thủ công trong SAT.
- Chỉ tạo `AlarmMismatch` khi hai trạng thái có cùng ý nghĩa, cùng threshold/source configuration đã xác nhận và không thống nhất quá thời gian cho phép. Không tạo mismatch chỉ vì SoftwareAlarm và controller đang dùng hai ngưỡng khác nhau có chủ ý.
- Alarm đang hoạt động nhưng chưa được acknowledge phải nhắc lại bằng âm thanh/hiển thị theo khoảng thời gian cấu hình, không tạo AlarmEvent mới và không thay đổi `StartedAtUtc`.

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
- Phiên bản đầu dùng bảng PostgreSQL thông thường với index phù hợp. Thiết kế migration phải sẵn sàng chuyển sang phân vùng theo tháng, nhưng chỉ bật partition nếu capacity test hoặc retention/vacuum không đạt SLA.
- Mỗi mẫu/sự kiện phải có khóa định danh chống ghi trùng khi ứng dụng phát lại dữ liệu sau sự cố kết nối database.
- M0 phải chốt RPO, RTO và thời gian mất database cần chịu được. Nếu yêu cầu không mất dữ liệu qua restart trong thời gian gián đoạn, dùng hàng đợi bền vững cục bộ với dung lượng theo RPO đã duyệt; nếu chấp nhận mất một khoảng ngắn, có thể dùng buffer đơn giản hơn. Mọi phương án phải cảnh báo khi database lỗi, quy định hành vi khi queue đầy và replay idempotent.

### FR-08 Biểu đồ và tra cứu

- Hiển thị trend của một hoặc nhiều tank.
- Chọn khoảng thời gian tùy ý.
- Hiển thị đường High/Low limit trên biểu đồ.
- Cho phép phóng to, thu nhỏ và xem giá trị tại từng thời điểm.
- Dữ liệu phải phân biệt giá trị tốt, mất kết nối và sensor error.

### FR-09 Xuất dữ liệu

- Xuất CSV là chức năng bắt buộc của phiên bản 1 và không phụ thuộc Microsoft Excel.
- Xuất `.xlsx` là P2 mặc định. Chỉ nâng thành P1 tại M0 khi thư viện tạo Excel đã được kiểm tra giấy phép và IT/pháp chế/Product Owner chấp thuận; CSV luôn đủ điều kiện nghiệm thu phiên bản 1.
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

- Không dùng một tài khoản Operator dùng chung nếu cần truy vết người acknowledge/thay đổi cấu hình.
- Hỗ trợ chuyển người dùng nhanh bằng mã nhân viên + PIN/mật khẩu hoặc Windows identity theo chính sách IT; thao tác acknowledge phải gắn với người đang đăng nhập tại thời điểm thao tác.
- Nếu dùng PIN: PIN phải được băm; giới hạn số lần nhập sai, áp dụng progressive delay hoặc khóa tạm, ghi audit lần thất bại và có quy trình Administrator unlock/reset. Việc khóa user không được dừng dashboard, polling hoặc hiển thị alarm.
- Phiên làm việc tự khóa sau thời gian không thao tác cấu hình được; dashboard vẫn được phép tiếp tục hiển thị và thu thập dữ liệu.
- Tài khoản Administrator dùng riêng, không sử dụng chung với tài khoản PostgreSQL hay tài khoản Windows service.

### FR-11 Nhật ký thao tác

- Ghi nhận đăng nhập và đăng xuất.
- Ghi thay đổi cấu hình tank, địa chỉ Modbus và thông số COM port.
- Ghi thay đổi ngưỡng High/Low.
- Ghi thao tác xác nhận alarm.
- Nếu cho phép ghi SV, phải lưu giá trị trước, giá trị sau, người thao tác và thời gian.

### FR-12 Backup và restore

- Backup PostgreSQL thủ công bằng quy trình được ứng dụng hoặc quản trị viên gọi qua `pg_dump`.
- Hỗ trợ backup tự động theo lịch nếu được bật; file backup phải lưu ngoài thư mục cài đặt ứng dụng và tuân theo thời hạn lưu do IT phê duyệt.
- Backup phải bao gồm schema, dữ liệu và thông tin phiên bản migration; không đưa mật khẩu database vào file backup hoặc command log.
- Kiểm tra file backup bằng cách `pg_restore` vào một database kiểm thử riêng trước khi xác nhận backup hợp lệ.
- Không restore đè trực tiếp database đang được ứng dụng sử dụng; phải đưa ứng dụng vào chế độ bảo trì, dừng tác vụ ghi và restore vào database đích đã xác định.
- Có hướng dẫn phục hồi khi thay máy tính.
- Quy trình phải nêu rõ phiên bản PostgreSQL/`pg_dump`/`pg_restore` tương thích và tài khoản được phép thực hiện.

### FR-13 Chẩn đoán và log

- Ghi lỗi mở COM port, timeout, CRC, Modbus exception và lỗi database.
- Không ghi mật khẩu dưới dạng rõ trong log.
- Có màn hình chẩn đoán cho quản trị viên.
- Cho phép xem request/response Modbus ở chế độ kỹ thuật; mặc định phải tắt để tránh file log quá lớn.

### FR-14 Chế độ bảo trì

- Administrator được đưa một tank, một tuyến hoặc toàn hệ thống vào Maintenance Mode.
- Khi bật phải nhập lý do, phạm vi, thời điểm bắt đầu, người thực hiện và thời gian tự hết hạn; mặc định không cho phép bảo trì vô thời hạn nếu chưa có quyền đặc biệt.
- Giao diện phải hiển thị Maintenance Mode rõ ràng, không được dùng màu giống trạng thái Normal.
- Polling và lưu lịch sử vẫn tiếp tục trừ khi người quản trị chọn dừng tuyến; quy tắc suppress âm thanh/thông báo phải cấu hình rõ, không xóa AlarmEvent hoặc che mất Communication Error ngoài phạm vi bảo trì.
- Mọi thao tác bật, gia hạn, kết thúc hoặc tự hết hạn phải ghi audit log.

### FR-15 Giám sát sức khỏe ứng dụng

- Hiển thị trạng thái polling, historian, PostgreSQL, buffer/queue theo RPO, dung lượng lưu trữ và thời điểm heartbeat gần nhất.
- Có cảnh báo khi queue tăng liên tục, backup thất bại, database gần đầy hoặc một background task ngừng hoạt động.
- Watchdog được phép khởi động lại ứng dụng theo chính sách đã phê duyệt nhưng phải giới hạn số lần, có backoff và lưu nguyên nhân.
- Có thao tác xuất gói chẩn đoán gồm log đã loại bỏ secret, phiên bản ứng dụng, cấu hình không nhạy cảm và trạng thái service để hỗ trợ xử lý sự cố.

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

### Areas

- Id.
- Code duy nhất.
- Name.
- Description.
- DisplayOrder.
- Enabled.

### Tanks

- Id.
- Code.
- Name.
- ChemicalType.
- AreaId.
- LocationDetail nếu cần mô tả vị trí chi tiết.
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
- OfflineProbeInterval.
- OfflineProbeBudgetPercent.

### Users

- Id.
- EmployeeCode/LoginName duy nhất.
- DisplayName.
- PasswordHash/PINHash nếu dùng tài khoản ứng dụng.
- Enabled, FailedAttemptCount, LockedUntilUtc.
- CreatedAtUtc và LastLoginAtUtc.

### Roles và UserRoles

- Roles: Id, Code (`Operator`, `Administrator`), Name.
- UserRoles: UserId, RoleId.
- Không dùng role database PostgreSQL để thay cho role người dùng ứng dụng.

### UserSessions

- Id.
- UserId.
- StartedAtUtc, LastActivityAtUtc, EndedAtUtc.
- AuthenticationMethod (`ApplicationPIN`, `Password`, `WindowsIdentity`).
- Workstation/WindowsIdentity nếu chính sách IT cho phép lưu.

### TemperatureSamples

- Id.
- TankId.
- SampledAtUtc (`timestamptz`).
- PV.
- SV.
- Quality.
- DeviceStatus.
- SourceEventId dùng để chống ghi trùng khi replay.

### AlarmEvents

- Id.
- ScopeType (`Tank`, `Device`, `Connection`, `System`).
- ScopeId; TankId có thể null khi alarm thuộc Connection/System.
- AlarmType.
- AlarmSource (`SoftwareAlarm`, `ControllerAlarm`, `AlarmMismatch`; `LocalHardwareAlarm` dành cho P2 khi có đường tín hiệu).
- CorrelationId và RootCauseAlarmId để gộp/suppress alarm con khi lỗi toàn tuyến.
- StartedAtUtc (`timestamptz`).
- EndedAtUtc (`timestamptz`, nullable).
- StartValue.
- EndValue.
- SoftwareLowLimit và SoftwareHighLimit.
- ControllerLowLimit và ControllerHighLimit nếu đọc được.
- AcknowledgedByUserId.
- AcknowledgedAtUtc (`timestamptz`, nullable).
- Note.

### MaintenancePeriods

- Id.
- ScopeType và ScopeId.
- Reason.
- StartedAtUtc.
- ExpiresAtUtc.
- EndedAtUtc.
- StartedByUserId và EndedByUserId.
- SuppressSound/SuppressNotification theo chính sách đã phê duyệt.

### ApplicationHealthEvents

- Id.
- Component.
- HealthStatus.
- OccurredAtUtc.
- Message.
- RecoveryAtUtc.
- CorrelationId.

### AuditLogs

- Id.
- CreatedAtUtc (`timestamptz`).
- UserId và UserDisplayName snapshot.
- Action.
- ObjectType.
- ObjectId.
- OldValue.
- NewValue.

### SystemSettings và AlarmPolicies

- SystemSettings lưu version cấu hình, polling profile, storage interval, timezone hiển thị, RPO/RTO và giới hạn queue đã phê duyệt.
- AlarmPolicies lưu reminder interval, hysteresis, delay, connection-alarm aggregation và maintenance suppression policy.
- Các setting an toàn/quan trọng phải có kiểu dữ liệu, validation và audit; không dùng chuỗi key/value tùy ý trong Form.

### SchemaMigrationHistory

- Do công cụ migration quản lý, tối thiểu có Version, AppliedAtUtc, Checksum và ApplicationVersion.
- Không chỉnh sửa thủ công migration đã chạy trên production; thay đổi tiếp theo phải là migration mới.

### LocalOutageQueue — lưu cục bộ, không nằm trong PostgreSQL

- QueueFormatVersion.
- EventId/SourceEventId duy nhất.
- CreatedAtUtc.
- RecordType và payload.
- Checksum.
- ReplayAttemptCount và LastReplayAtUtc.
- Quy tắc atomic write, giới hạn dung lượng, xử lý record hỏng và hành vi khi đầy theo RPO/RTO đã phê duyệt.

### Quy ước PostgreSQL

- Tất cả thời điểm lưu trong database dùng UTC với kiểu `timestamptz`; chỉ chuyển sang múi giờ Việt Nam tại lớp hiển thị/xuất báo cáo.
- Tên bảng/cột phải thống nhất một quy ước, đề xuất `snake_case`; không phụ thuộc chữ hoa/thường trong câu SQL.
- Khóa chính dùng `bigint generated ... as identity` hoặc UUID theo một lựa chọn thống nhất; khóa ngoại và index phải được khai báo rõ.
- `temperature_samples` ban đầu là bảng thường có index tổng hợp `tank_id, sampled_at_utc`. Chỉ chuyển sang partition theo tháng nếu capacity test/retention/vacuum chứng minh cần thiết.
- Có unique constraint/index phù hợp cho mã tank, địa chỉ Modbus trong cùng connection và `source_event_id` để ngăn dữ liệu replay bị trùng.
- Nhiệt độ và ngưỡng phải dùng kiểu số có độ chính xác xác định, không dùng kiểu chuỗi; scale từ controller được xử lý trước khi ghi.
- Mỗi migration chỉ chạy một lần và được ghi vào bảng lịch sử migration.
- Hàng đợi bền vững, nếu được yêu cầu theo RPO/RTO, là vùng đệm kỹ thuật cục bộ chứ không phải database nghiệp vụ thứ hai.

## 10. Yêu cầu phi chức năng

### NFR-01 Hiệu năng

- UI phải phản hồi bình thường trong khi polling thiết bị.
- Thời gian hiển thị giá trị mới không lớn hơn một chu kỳ polling cộng thời gian đọc toàn tuyến.
- Tra cứu dữ liệu một tháng của một tank phải hoàn thành trong thời gian mục tiêu dưới 3 giây trên máy vận hành tiêu chuẩn.
- Việc xuất dữ liệu lớn phải chạy nền và có thông báo tiến độ.
- Ghi mẫu phải dùng transaction ngắn, batch hợp lý và connection pooling; không mở một connection mới cho từng tank.
- Bảng mẫu phải có index phục vụ truy vấn theo `TankId + Timestamp`; capacity test bắt đầu với bảng không partition và dữ liệu mô phỏng tối thiểu 12–24 tháng cho 40 tank. Partition chỉ được bật khi có báo cáo chứng minh cần thiết.
- Polling test phải đo p50/p95/p99 của chu kỳ bus và chứng minh online traffic cộng offline-probe budget vẫn đáp ứng chu kỳ đã phê duyệt.

### NFR-02 Độ ổn định

- Không crash khi mất COM port, rút USB-RS485, controller mất nguồn, PostgreSQL restart hoặc mất kết nối mạng tới database.
- Ứng dụng phải tự phục hồi kết nối khi điều kiện bình thường trở lại.
- Một thiết bị lỗi không được chặn polling các thiết bị khác.
- Một tuyến RS485 lỗi không được làm dừng tuyến khác.
- Lỗi lưu database không được làm dừng polling; trạng thái `Database Disconnected/Degraded` phải hiển thị rõ và được ghi log.
- Việc ghi bù từ hàng đợi phải có cơ chế idempotent để không tạo sample, alarm hoặc audit log trùng.

### NFR-03 An toàn dữ liệu

- Giao dịch database phải bảo đảm không tạo bản ghi alarm dở dang.
- Backup phải được kiểm tra có thể mở lại.
- Không xóa dữ liệu vận hành nếu chưa có xác nhận và chính sách lưu trữ.
- Đồng hồ máy tính phải được đồng bộ thời gian theo chính sách IT của nhà máy.
- Timestamp phải lưu bằng `timestamptz` theo UTC; giao diện và file xuất hiển thị theo múi giờ vận hành Việt Nam.
- Migration phải được chạy có kiểm soát, có log và có backup trước thay đổi schema trên môi trường production.

### NFR-04 Bảo mật

- Chỉ Administrator được thay đổi cấu hình quan trọng.
- Mật khẩu phải lưu bằng phương pháp băm, không lưu dạng rõ.
- Ứng dụng dùng tài khoản PostgreSQL riêng với quyền tối thiểu; không dùng tài khoản `postgres` khi vận hành.
- Chuỗi kết nối và mật khẩu database không được hard-code hoặc lưu dạng rõ; trên Windows phải bảo vệ bằng DPAPI/Windows Credential Manager hoặc giải pháp quản lý secret do IT phê duyệt.
- Nếu PostgreSQL chạy cùng máy, chỉ cho phép kết nối loopback trừ khi IT phê duyệt khác. Nếu chạy trên máy chủ riêng, chỉ mở cổng database cho đúng máy ứng dụng, ưu tiên TLS và tuyệt đối không public ra Internet.
- Truy cập từ xa phải dùng mạng nội bộ hoặc VPN và được IT Samsung phê duyệt.

### NFR-05 Khả năng bảo trì

- Mã nguồn phải chia module và không đặt logic truyền thông trực tiếp trong Form.
- Thông tin register phải tách khỏi giao diện, có thể thay đổi theo model hoặc firmware.
- Có unit test cho CRC16, parse register, alarm state machine và lưu dữ liệu.
- Có simulator để phát triển và kiểm thử khi không có thiết bị thật.
- Simulator phải fault-injection được: no response, delay, bad CRC, fragmented frame, Modbus exception, negative value, sensor error và disconnect/recovery.

### NFR-06 Triển khai

- Baseline là Windows 11 x64 hoặc phiên bản Windows còn được nhà cung cấp hỗ trợ và được IT Samsung phê duyệt. Windows 10 chỉ được dùng khi thuộc LTSC/ESU còn hiệu lực và có chấp thuận bằng văn bản của IT.
- Hỗ trợ độ phân giải 1920 x 1080 và DPI scaling thông dụng.
- Có bộ cài đặt và quy trình nâng cấp không làm mất database/configuration.
- Có tùy chọn tự khởi động cùng Windows.
- Bộ cài phải kiểm tra PostgreSQL service, khả năng kết nối, phiên bản schema và quyền ghi trước khi cho phép vận hành.
- Không tự động cài PostgreSQL hoặc thay đổi `pg_hba.conf`, firewall và tài khoản hệ thống nếu chưa được IT phê duyệt.

### NFR-07 Dependency và giấy phép

- Trước khi khóa Release Candidate phải có danh sách toàn bộ NuGet package, runtime, chart/export library, installer tool và license tương ứng.
- Chỉ dùng dependency có nguồn rõ ràng, phiên bản được khóa và còn được bảo trì; không tự động nâng version production nếu chưa chạy regression test.
- CSV là định dạng xuất bắt buộc không cần thư viện thương mại. Excel, chart nâng cao hoặc installer thương mại chỉ được đưa vào bản release sau khi license được xác nhận.
- Phải tạo Software Bill of Materials hoặc bảng dependency tối thiểu gồm tên, version, nguồn, license, mục đích và trạng thái phê duyệt.
- Kiểm tra lỗ hổng dependency trước Release Candidate; phát hiện mức Critical/High phải được xử lý hoặc có phê duyệt ngoại lệ bằng văn bản.

### NFR-08 Ngôn ngữ và khả dụng giao diện

- Giao diện Operator mặc định dùng tiếng Việt; mã lỗi, tên register và thuật ngữ giao thức có thể giữ tiếng Anh khi cần chính xác kỹ thuật.
- Chuỗi hiển thị phải đặt trong resource `.resx`, không hard-code trong Form, để có thể bổ sung tiếng Anh sau mà không sửa logic.
- Thuật ngữ alarm, maintenance, acknowledge và trạng thái kết nối phải thống nhất trong toàn bộ màn hình, báo cáo và tài liệu hướng dẫn.
- Thư viện chart phải vượt spike license/hiệu năng trước khi phát triển trend; dữ liệu dài hạn phải được downsample để không nạp hàng triệu điểm trực tiếp vào UI.

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
13. Chế độ bảo trì và lịch sử bảo trì.
14. Sức khỏe ứng dụng, PostgreSQL, buffer/queue và watchdog.
15. Chuyển người dùng nhanh để acknowledge alarm.

## 12. Điều kiện đầu vào M0 trước khi bắt đầu tính thời gian phát triển

Các điều kiện sau cần có hoặc cần được xác nhận:

- Chốt số lượng phiên bản 1 là 21 tank.
- Danh sách mã tank, vị trí và loại hóa chất.
- Danh sách Area/khu vực và mapping mỗi tank vào một Area.
- Xác nhận model controller thực tế có RS485.
- Xác nhận một tuyến hay nhiều tuyến RS485.
- Danh sách địa chỉ Modbus của từng controller.
- Communication user manual hoặc register map chính thức.
- Bảng register đã ghi rõ function code, address, length, signed/unsigned, scale, decimal, unit, bit mask, sensor error và nguồn tài liệu.
- Xác nhận cấu hình AL1/AL2 của từng controller; không mặc định AL1/AL2 tương ứng High/Low.
- Chốt nguồn chuẩn: controller threshold cho bảo vệ cục bộ, software threshold cho giám sát; xác nhận có đọc được controller alarm setpoint hay chỉ kiểm tra thủ công trong SAT.
- Xác nhận V1 không có LocalHardwareAlarm nếu chưa có DI/gateway/tín hiệu đầu vào.
- Một controller TK4W RS485 thật.
- Một bộ chuyển đổi USB-RS485 hoặc gateway sử dụng thực tế.
- Xác nhận ngưỡng High/Low của từng tank.
- Xác nhận có cho phép phần mềm ghi SV hay chỉ đọc.
- Xác nhận chu kỳ lưu và thời gian lưu dữ liệu.
- Xác nhận định dạng báo cáo cần xuất.
- Xác nhận phiên bản Windows, chính sách cài đặt và quyền Administrator trên máy vận hành.
- Xác nhận yêu cầu tài khoản người dùng và phân quyền.
- Chọn cơ chế định danh người vận hành: tài khoản ứng dụng + PIN hoặc Windows identity; không dùng tài khoản chung nếu cần audit.
- Chốt PostgreSQL local hay server tập trung, tài khoản dịch vụ, TLS/firewall, backup và quyền IT.
- Chốt RPO/RTO khi PostgreSQL gián đoạn, thời gian buffer cần giữ và hành vi khi queue đầy; quyết định có cần durable queue hay không.
- Chốt CSV là bắt buộc; quyết định Excel dựa trên thư viện và giấy phép được phê duyệt.
- Hoàn thành kiểm tra license/dependency dự kiến cho Modbus, chart, Excel, PostgreSQL client, logging và installer.
- Chọn và khóa version thư viện chart sau spike license, DPI, realtime, downsampling và hiệu năng.
- Chốt ngôn ngữ V1 là tiếng Việt và phạm vi thuật ngữ tiếng Anh kỹ thuật.
- Chốt phương án watchdog phiên bản 1; xác nhận có yêu cầu thu thập khi chưa đăng nhập Windows hay không.
- Repository đã có `.gitignore` loại trừ `docs-private/`, secret, `.vs`, `bin`, `obj`, log, backup dump, export và dữ liệu runtime; PDF/DOCX public không bị ignore toàn cục; các file generated đã được bỏ theo dõi khỏi Git.

Nếu chưa có thiết bị thật, có thể phát triển với simulator nhưng không được nghiệm thu chức năng truyền thông cho đến khi kiểm thử trên phần cứng thực tế.

M0 không tính vào 25 ngày phát triển. Nếu một đầu vào P0 còn thiếu, task phụ thuộc phải ở trạng thái Blocked và tiến độ chính thức chưa bắt đầu hoặc được điều chỉnh bằng change request.

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
- Spike block read và single-register read có kết quả đo trên controller thật; phương án được chọn có bằng chứng và fallback đã kiểm thử.
- Thiết bị Offline dùng backoff/probe, không làm chậm bất hợp lý các thiết bị Online.
- Test giá trị âm, decimal position, sensor error, fragmented frame và Modbus exception đạt yêu cầu.

### 13.3 Hoàn thành alarm

- Mô phỏng và kiểm thử High, Low, sensor error và communication error.
- Alarm được tạo một lần khi bắt đầu, không tạo bản ghi lặp ở mỗi chu kỳ polling.
- Ghi đúng thời gian bắt đầu, kết thúc và xác nhận.
- Hysteresis và delay hoạt động đúng.
- Sau khi khởi động lại ứng dụng, trạng thái alarm phải được khôi phục hợp lý từ điều kiện thực tế và database.
- Phân biệt được SoftwareAlarm, ControllerAlarm và AlarmMismatch; LocalHardwareAlarm được đánh dấu Not Applicable cho V1 nếu không có DI/gateway.
- Lỗi toàn tuyến tạo một Connection/Bus Alarm gốc; không gây alarm flood 21 âm thanh/sự kiện độc lập.
- AlarmMismatch chỉ áp dụng cho các nguồn/threshold thực sự có thể so sánh theo cấu hình đã xác nhận.
- Reminder không tạo AlarmEvent trùng và không thay đổi thời điểm bắt đầu alarm.
- Maintenance Mode hiển thị rõ, có lý do/người/thời hạn và đầy đủ audit log.

### 13.4 Hoàn thành dữ liệu

- Lưu dữ liệu liên tục đúng chu kỳ.
- Không lưu giá trị lỗi như giá trị nhiệt độ hợp lệ.
- Tra cứu và xuất dữ liệu cho kết quả đúng.
- Backup tạo thành công và restore được trên môi trường kiểm thử.
- Việc nâng cấp phiên bản không làm mất dữ liệu đã có.
- PostgreSQL restart/mất mạng không làm dừng polling; buffer/queue theo RPO đã phê duyệt và ghi bù không trùng.
- Capacity test tối thiểu 12–24 tháng/40 tank trên bảng không partition đạt mục tiêu hoặc cung cấp bằng chứng cần bật partition.
- Timestamp lưu UTC và hiển thị đúng múi giờ vận hành Việt Nam.

### 13.5 Hoàn thành kiểm thử

- Unit test quan trọng chạy thành công.
- Integration test với ít nhất một controller thật hoàn thành.
- Test nhiều địa chỉ bằng thiết bị thật hoặc simulator hoàn thành.
- Chạy liên tục tối thiểu 72 giờ mà không có crash hoặc mất dữ liệu nghiêm trọng.
- UAT được người đại diện vận hành xác nhận.
- Simulator fault injection bao phủ no response, delay, bad CRC, fragmented frame, Modbus exception, negative value, sensor error và recovery.
- Watchdog/Task Scheduler, giới hạn restart và heartbeat của background task đã được kiểm thử.
- Ma trận truy vết không còn yêu cầu P0/P1 bắt buộc thiếu task, test case hoặc bằng chứng kết quả.

### 13.6 Hoàn thành bàn giao

- Bàn giao mã nguồn và file solution.
- Bàn giao bản release và installer.
- Bàn giao cấu hình mẫu.
- Bàn giao database schema và hướng dẫn backup/restore.
- Bàn giao tài liệu cài đặt, vận hành và xử lý sự cố cơ bản.
- Bàn giao danh sách dependency và license liên quan.
- Bàn giao ma trận truy vết yêu cầu–task–test và Software Bill of Materials/dependency register.
- Đào tạo người vận hành và quản trị viên.

### 13.7 Tiêu chí nghiệm thu định lượng — nguồn chuẩn duy nhất

| ID | Nhóm | Tiêu chí bắt buộc |
|---|---|---|
| QC-01 | Build | Build Release x64 thành công, không có lỗi biên dịch và chạy trên máy không có Visual Studio |
| QC-02 | Driver | CRC, frame accumulator, exception, cancellation và retry vượt toàn bộ test bắt buộc |
| QC-03 | Độ chính xác | PV/SV, giá trị âm, scale, decimal và sensor error khớp controller/register map |
| QC-04 | Polling | p95 chu kỳ 21 tank đáp ứng chu kỳ được chốt từ spike; không cam kết 2 giây nếu single read không đủ ngân sách |
| QC-05 | Offline budget | Offline probe không vượt 10–15% ngân sách bus và không làm thiết bị Online trễ quá SLA |
| QC-06 | Mở rộng | Simulator/test chạy 40 tank trên ít nhất hai connection độc lập |
| QC-07 | UI | Không treo khi timeout/export; hiển thị rõ ở độ phân giải và DPI do IT phê duyệt |
| QC-08 | Reconnect | Tự phục hồi sau khi đường truyền trở lại; mục tiêu không quá 30 giây trừ backoff đã phê duyệt |
| QC-09 | Alarm | Không tạo alarm trùng; lưu đúng source/scope/start/end/acknowledge/correlation và gộp lỗi toàn tuyến |
| QC-10 | Maintenance | Có reason/user/start/end/expiry/audit; trạng thái luôn dễ nhận biết và không xóa alarm |
| QC-11 | Dữ liệu | Không lưu giá trị lỗi như PV hợp lệ; UTC và tank/area mapping chính xác |
| QC-12 | Query | Truy vấn một tháng của một tank dưới 3 giây trên máy/database chuẩn đã xác định |
| QC-13 | Export | CSV mở được không cần Excel và khớp dữ liệu lọc; Excel chỉ nghiệm thu nếu được nâng thành P1 tại M0 |
| QC-14 | Backup | `pg_dump` được `pg_restore` thành công vào database kiểm thử và ứng dụng đọc được dữ liệu |
| QC-15 | PostgreSQL outage | Polling tiếp tục; buffer/queue đáp ứng RPO đã duyệt và replay idempotent |
| QC-16 | Capacity | Dataset 12–24 tháng/40 tank đạt SLA trên bảng thường hoặc có báo cáo phê duyệt partition |
| QC-17 | Ổn định | Chạy liên tục tối thiểu 72 giờ không crash, treo UI hoặc mất dữ liệu vượt RPO |
| QC-18 | Bảo mật | Operator không sửa cấu hình quản trị; database role quyền tối thiểu; secret không xuất hiện trong config/log/package |
| QC-19 | Định danh | Acknowledge/config lưu đúng UserId; PIN có rate limit/lockout/audit nếu được sử dụng |
| QC-20 | Watchdog | Heartbeat, startup, restart limit/backoff và log nguyên nhân hoạt động theo phương án đã khóa tại M0 |
| QC-21 | Dependency | Có SBOM/dependency register; không còn license hoặc lỗ hổng Critical/High chưa xử lý/waive |
| QC-22 | Traceability | Mọi FR/NFR P0/P1 có task, test ID, verification status và bằng chứng |
| QC-23 | UAT | Không còn lỗi Critical/High và có chữ ký đại diện vận hành/IT theo trách nhiệm |

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
| AT-16 | Giá trị âm và decimal position | PV/SV sau scale khớp controller, không bị chuyển thành số unsigned |
| AT-17 | So sánh block read và single read | Phương án được chọn đọc đúng dữ liệu, đạt chu kỳ quét và fallback hoạt động |
| AT-18 | Nhiều thiết bị Offline | Thiết bị Online vẫn được cập nhật; Offline được probe theo backoff và không retry vô hạn |
| AT-19 | Controller alarm khác SoftwareAlarm | Hiển thị đúng từng nguồn; chỉ tạo AlarmMismatch khi hai nguồn có cùng cấu hình so sánh đã xác nhận |
| AT-20 | Alarm chưa acknowledge | Reminder hoạt động nhưng không tạo sự kiện trùng hoặc đổi StartedAtUtc |
| AT-21 | Maintenance Mode | Hiển thị rõ phạm vi/lý do/người/thời hạn; tự hết hạn và ghi audit đầy đủ |
| AT-22 | PostgreSQL restart/mất mạng | Polling tiếp tục, buffer/queue đáp ứng RPO đã duyệt và ghi bù không trùng khi phục hồi |
| AT-23 | Dữ liệu 12–24 tháng/40 tank | Query đạt mục tiêu trên bảng thường; partition chỉ bật khi báo cáo chứng minh cần thiết |
| AT-24 | Watchdog dừng ứng dụng/background task | Phát hiện, cảnh báo/restart theo chính sách, có backoff và log nguyên nhân |
| AT-25 | Chuyển Operator và acknowledge | Audit lưu đúng người thực hiện; không chấp nhận danh tính dùng chung không truy vết được |
| AT-26 | Gói chẩn đoán và raw-frame log | Có version/config không nhạy cảm/log cần thiết, không chứa secret; raw frame mặc định tắt và có rotation |
| AT-27 | Security/least privilege | Role ứng dụng không phải superuser, secret được bảo vệ, firewall/TLS đúng chính sách và thao tác vượt quyền bị từ chối |
| AT-28 | Kiến trúc và simulator | Logic nghiệp vụ không nằm trong Form; unit/fault-injection test chạy no response, CRC, fragmented frame, exception và recovery |
| AT-29 | Cài đặt/nâng cấp/DPI | Cài mới, nâng cấp, gỡ cài đặt, startup/watchdog và DPI trên Windows được IT phê duyệt đều đạt |
| AT-30 | PIN sai nhiều lần | Progressive delay/khóa tạm/audit/admin unlock hoạt động; dashboard và polling không dừng |
| AT-31 | Mất toàn bộ tuyến RS485 | Tạo một Connection/Bus Alarm gốc, tank chuyển Offline nhưng không phát 21 alarm âm thanh độc lập |
| AT-32 | Đối chiếu threshold | Hiển thị riêng controller/software threshold; nếu không đọc được controller setpoint thì có checklist SAT thủ công |
| AT-33 | Dependency/license/SBOM | Mọi package/tool có version, nguồn, license, trạng thái duyệt; không còn lỗ hổng Critical/High chưa xử lý/waive |
| AT-34 | Ngôn ngữ và chart | UI Operator tiếng Việt từ `.resx`; chart realtime/lịch sử đạt DPI, downsampling và ngân sách CPU/RAM đã chốt |

## 15. Sản phẩm bàn giao

- Source code C# WinForms.
- File solution và project.
- Script hoặc migration tạo database.
- Bộ script tạo role/database, cấp quyền tối thiểu và cấu hình kết nối PostgreSQL mẫu không chứa mật khẩu thật.
- Bộ cài đặt bản Release x64.
- File cấu hình mẫu cho 21 tank.
- Simulator hoặc công cụ test Modbus dùng trong phát triển.
- Test cases và kết quả kiểm thử.
- Tài liệu cài đặt.
- Tài liệu hướng dẫn vận hành.
- Tài liệu cấu hình thiết bị và địa chỉ Modbus.
- Tài liệu backup, restore và xử lý sự cố.
- Tài liệu cài đặt/vận hành PostgreSQL, migration, theo dõi dung lượng và xử lý mất kết nối database.
- Danh sách phiên bản, dependency và giấy phép sử dụng.

## 16. Kế hoạch thực hiện khi sử dụng AI hỗ trợ

Giả định một lập trình viên WinForms có kinh nghiệm, làm việc toàn thời gian và sử dụng AI để hỗ trợ tạo cấu trúc, UI, database, test và tài liệu.

| Giai đoạn | Công việc | Thời gian dự kiến |
|---|---|---:|
| M0 | Chốt phạm vi, register map, ngưỡng, hạ tầng PostgreSQL và quyền IT | Không tính trong 25 ngày |
| 1 | Driver Modbus RTU, simulator, block-read spike, offline backoff | 5 ngày |
| 2 | Kiến trúc, PostgreSQL, outage buffer/queue và dashboard | 5 ngày |
| 3 | Alarm sources/mismatch, maintenance, historian và báo cáo | 5 ngày |
| 4 | Người dùng/audit, backup, watchdog, dependency và kiểm thử tích hợp | 5 ngày |
| 5 | Hardening, capacity test, tài liệu, regression và Release Candidate | 5 ngày |
| SAT/UAT | Cài đặt, chạy ổn định 72 giờ, đào tạo và nghiệm thu | 5 ngày |

Ước lượng tổng:

- Prototype/MVP nội bộ: 10–12 ngày làm việc, chưa đủ điều kiện triển khai production.
- Bản vận hành đầy đủ: 25 ngày phát triển + 5 ngày SAT/UAT, tương đương khoảng 6 tuần khi M0 đã hoàn tất.
- Nếu tách acquisition thành Windows Service: cộng 3–5 ngày phát triển và kiểm thử installer/recovery.
- Với PostgreSQL, ước lượng giả định server/service, tài khoản, firewall/TLS và quyền backup đã sẵn sàng trước ngày phát triển đầu tiên; nếu chưa sẵn sàng, cộng 1–3 ngày kỹ thuật và toàn bộ thời gian chờ IT.
- Nếu thiếu register map hoặc chưa có thiết bị thật: cộng thêm 3–7 ngày làm việc.
- Nếu bổ sung ghi SV hoặc giám sát từ xa: cộng thêm khoảng 1–2 tuần tùy yêu cầu bảo mật và phê duyệt IT.

AI giúp rút ngắn việc tạo mã nguồn, UI, database, test và tài liệu. AI không thay thế được kiểm thử RS485, xác nhận register map, kiểm tra controller/alarm thực tế, phê duyệt IT/license và UAT với người vận hành.

## 17. Rủi ro project

| Rủi ro | Mức ảnh hưởng | Biện pháp |
|---|---|---|
| Controller thực tế là R4RN không có RS485 | Cao | Xác nhận model trước khi lập trình driver hoặc bổ sung kiến trúc analog/gateway |
| Thiếu register map Modbus | Cao | Lấy communication manual và sample C# chính thức |
| Không có thiết bị thật để kiểm thử | Cao | Dùng simulator trong phát triển, bắt buộc test thật trước nghiệm thu |
| Trùng địa chỉ Modbus | Cao | Kiểm tra cấu hình và validate trong phần mềm |
| Nhiễu hoặc timeout RS485 | Cao | Retry, reconnect, log lỗi và phối hợp kiểm tra hệ thống cáp |
| Đọc sai signed/scale/register | Cao | Register map có nguồn, test vector giá trị âm/decimal/sensor error và đối chiếu controller thật |
| Một thiết bị Offline làm chậm toàn tuyến | Cao | Retry giới hạn, offline backoff/probe và đo thời gian quét 21/40 thiết bị |
| Controller alarm khác SoftwareAlarm | Cao | Lưu riêng nguồn alarm, xác nhận AL1/AL2 và cảnh báo AlarmMismatch |
| Phạm vi thay đổi từ 21 lên 40 tank | Trung bình | Thiết kế multi-connection ngay từ đầu |
| Ngưỡng alarm chưa được phê duyệt | Cao | Chủ quản công nghệ/EHS xác nhận trước UAT |
| Yêu cầu remote access phát sinh muộn | Trung bình | Tách thành giai đoạn 2 và thực hiện đánh giá bảo mật |
| Database tăng nhanh | Trung bình | Index + capacity test bảng thường; chỉ partition khi SLA/retention chứng minh cần, kèm backup/cảnh báo dung lượng |
| PostgreSQL service dừng hoặc mất kết nối mạng | Cao | Chốt RPO/RTO; health check, reconnect, buffer/durable queue phù hợp và replay idempotent |
| Lộ mật khẩu database | Cao | Tài khoản quyền tối thiểu, DPAPI/Credential Manager, không log connection string và xoay vòng mật khẩu |
| Máy tính bị tắt hoặc Windows Update | Trung bình | Auto-start, giám sát ứng dụng và thống nhất chính sách IT |
| Watchdog restart lặp vô hạn | Cao | Giới hạn số lần, exponential backoff, health event và yêu cầu can thiệp sau ngưỡng |
| Audit không xác định được người thao tác | Cao | Cấm tài khoản dùng chung, quick switch/Windows identity và session timeout |
| Dependency/license không được phê duyệt | Trung bình | Review sớm, CSV bắt buộc, khóa version và bàn giao SBOM/dependency register |
| Thay đổi SV không được kiểm soát | Cao | Phiên bản đầu read-only; nếu mở ghi phải có phân quyền và audit |

## 18. Các quyết định cần xác nhận

- [ ] Phạm vi chính thức là 21 tank hay 40 tank.
- [ ] Model controller chính xác của từng tank.
- [ ] Controller có RS485 hay chỉ có 4-20 mA.
- [ ] Có bao nhiêu tuyến RS485 và COM port/gateway.
- [ ] Register map chính thức đã được cung cấp.
- [ ] Cấu hình AL1/AL2, sensor error, signed/scale/decimal/đơn vị đã được xác nhận trên controller thật.
- [ ] Kết quả spike block read/single read đã xác định chiến lược polling và fallback.
- [ ] Có cho phép ghi SV từ phần mềm.
- [ ] Ngưỡng High/Low của từng tank đã được phê duyệt.
- [ ] Chu kỳ polling.
- [ ] Chu kỳ lưu và thời hạn lưu dữ liệu.
- [ ] PostgreSQL chạy cục bộ trên máy giám sát hay trên máy chủ tập trung.
- [ ] Phiên bản PostgreSQL, cổng kết nối, TLS, firewall và tài khoản dịch vụ đã được IT phê duyệt.
- [ ] Vị trí lưu backup, lịch backup, thời hạn giữ backup và người chịu trách nhiệm kiểm tra restore.
- [ ] Yêu cầu file báo cáo.
- [ ] Cơ chế tài khoản ứng dụng/Windows identity, PIN, timeout phiên và quy trình cấp/thu hồi tài khoản đã được IT phê duyệt.
- [ ] Cơ chế định danh Operator là tài khoản ứng dụng + PIN hay Windows identity.
- [ ] Khoảng reminder alarm, timeout phiên người dùng và quy tắc Maintenance Mode.
- [ ] Phiên bản 1 dùng WinForms + watchdog hay bắt buộc Windows Service chạy khi chưa đăng nhập.
- [ ] Danh sách dependency và license đã được IT/pháp chế chấp thuận; Excel có thuộc phạm vi P1 hay không.
- [ ] Có cần chạy toàn màn hình và khóa thao tác ngoài ứng dụng.
- [ ] Có cần giám sát từ xa trong giai đoạn sau.
- [ ] Máy tính, Windows và chính sách cài đặt đã được IT xác nhận.
- [ ] Người đại diện nghiệm thu và quy trình UAT đã được xác định.

## 19. Mức ưu tiên và truy vết

### P0 — phải chốt trước hoặc trong tuần đầu, không được phát hành nếu thiếu

- Register map, signed/scale/decimal/sensor error và cấu hình AL1/AL2 có bằng chứng.
- Spike block read/single read, đo throughput và chiến lược offline backoff.
- Mô hình nguồn alarm, nguồn chuẩn threshold, alarm cấp tuyến và điều kiện bật AlarmMismatch.
- PostgreSQL, UTC, backup/restore và quyền tối thiểu; RPO/RTO quyết định loại buffer/durable queue.
- Ma trận truy vết `FR/NFR → task → test → bằng chứng`.
- M0 hoàn thành trước khi bắt đầu 25 ngày phát triển.
- Git hygiene: tài liệu riêng tư, secret và generated files không được đưa vào commit mới.
- Quyết định WinForms watchdog hay Windows Service.

### P1 — bắt buộc cho bản production trong phạm vi phiên bản 1

- Maintenance Mode có thời hạn và audit.
- Alarm reminder không tạo sự kiện trùng.
- Nhận diện đúng Operator, quick switch và session timeout.
- Index/capacity test 12–24 tháng/40 tank; partition chỉ khi có bằng chứng cần thiết.
- Review dependency/license, CSV bắt buộc và lựa chọn chart library.
- Watchdog, heartbeat, application health và gói chẩn đoán.

### P2 — giai đoạn sau hoặc change request

- Excel mặc định P2; chỉ nâng P1 bằng quyết định M0.
- LocalHardwareAlarm khi bổ sung DI/gateway/tín hiệu đầu vào.
- Remote access, email/SMS/Teams/Zalo notification.
- Ghi SV, điều khiển/liên động từ phần mềm.
- MES/SCADA, web/mobile, high availability và Windows Service nếu phát sinh yêu cầu vận hành mới.

Ma trận chi tiết được quản lý tại [Traceability Matrix SEMV](./Traceability-Matrix-SEMV.md). Tài liệu yêu cầu này là nguồn chuẩn cho FR/NFR và Definition of Done; lộ trình chỉ tham chiếu, không được tự thay đổi tiêu chí nghiệm thu.

## 20. Nguyên tắc quản lý thay đổi

- Yêu cầu mới sau khi chốt phạm vi phải được ghi thành change request.
- Change request phải mô tả mục tiêu, mức ưu tiên, tác động đến dữ liệu, UI, thiết bị và bảo mật.
- Các nội dung như remote access, ghi SV, tích hợp hệ thống ngoài hoặc thay đổi database trung tâm được xem là thay đổi phạm vi đáng kể.
- Tiến độ chỉ được chốt chính thức sau khi hoàn thành các mục trong phần Điều kiện đầu vào.
- Mọi thay đổi P0/P1 phải cập nhật đồng thời tài liệu yêu cầu, lộ trình, ma trận truy vết và test case liên quan.

## 21. Kết luận

Phiên bản 1 nên tập trung vào giám sát ổn định tại chỗ: đọc 21 controller RS485, dashboard, cảnh báo, lịch sử, biểu đồ, tra cứu, export, backup và chẩn đoán kết nối. Kiến trúc phải hỗ trợ tối thiểu 40 tank và nhiều tuyến RS485 ngay từ đầu.

Điều kiện quan trọng nhất để đảm bảo kế hoạch 25 ngày phát triển + 5 ngày SAT/UAT là hoàn thành M0 trước ngày phát triển đầu tiên: register map chính thức, controller RS485 thật, bộ chuyển đổi sử dụng thực tế, cấu hình nguồn alarm, PostgreSQL/quyền IT và dependency/license đã được xác nhận.
