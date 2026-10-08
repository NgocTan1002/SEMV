# Lộ trình hoàn thiện project phần mềm giám sát nhiệt độ hóa chất SEMV

## 1. Thông tin kế hoạch

| Nội dung | Giá trị |
|---|---|
| Project | Phần mềm giám sát nhiệt độ hóa chất SEMV |
| Loại ứng dụng | Windows Forms desktop application |
| Nền tảng đề xuất | C# và .NET 10 LTS, Windows x64 |
| Phạm vi phiên bản 1 | Giám sát 21 tank, mở rộng được đến tối thiểu 40 tank |
| Giao tiếp | RS485 Modbus RTU với Autonics TK Series |
| Cơ sở dữ liệu đề xuất | SQLite cho một máy giám sát |
| Thời gian phát triển mục tiêu | 20 ngày làm việc |
| Dự phòng hiện trường | 5 ngày làm việc |
| Tổng thời gian kế hoạch | 4 tuần phát triển và 1 tuần dự phòng |
| Phương thức thực hiện | Một lập trình viên WinForms có AI hỗ trợ, phối hợp kỹ thuật tự động hóa và người vận hành |
| Ngày lập kế hoạch | 08/10/2026 |

Tài liệu này triển khai chi tiết từ [Yêu cầu project phần mềm giám sát nhiệt độ hóa chất SEMV](./Yeu-cau-project-phan-mem-giam-sat-hoa-chat-SEMV.md).

## 2. Mục tiêu hoàn thành

Sau khi kết thúc lộ trình, project phải có một bản phần mềm WinForms có thể cài đặt trên máy giám sát và thực hiện được các chức năng sau:

- Kết nối ổn định với các bộ điều khiển Autonics TK Series qua RS485 Modbus RTU.
- Đọc và hiển thị nhiệt độ PV của 21 tank theo thời gian thực.
- Đọc SV và trạng thái alarm nếu register map/model thiết bị hỗ trợ.
- Nhận biết High Alarm, Low Alarm, Sensor Error và Communication Error.
- Lưu lịch sử nhiệt độ, alarm và lỗi truyền thông.
- Hiển thị biểu đồ, tra cứu và xuất dữ liệu.
- Quản lý cấu hình tank, kết nối và ngưỡng cảnh báo.
- Phân quyền người dùng cơ bản, audit log, backup và restore.
- Tự động kết nối lại sau sự cố và tiếp tục hoạt động khi một thiết bị bị lỗi.
- Có installer, tài liệu vận hành, mã nguồn và biên bản UAT.
- Kiến trúc hỗ trợ tối thiểu 40 tank và nhiều tuyến RS485.

## 3. Hiện trạng ban đầu

### 3.1 Thành phần đã có

- Chương trình mẫu chính thức của Autonics.
- Ví dụ CRC16 Modbus RTU.
- Ví dụ đọc model name, PV, SV và Alarm 1.
- Ví dụ ghi SV, RUN/STOP và Auto Tuning.
- Manual TK Series và tài liệu yêu cầu ban đầu.

### 3.2 Thành phần chưa có

- Chưa có ứng dụng WinForms vận hành thực tế.
- Chưa có solution theo kiến trúc nhiều lớp.
- Chưa có driver truyền thông bất đồng bộ, retry và reconnect hoàn chỉnh.
- Chưa có simulator phục vụ kiểm thử.
- Chưa có database.
- Chưa có dashboard 21/40 tank.
- Chưa có alarm engine.
- Chưa có lịch sử, biểu đồ, export, backup và restore.
- Chưa có installer và quy trình UAT.

### 3.3 Hạn chế của chương trình mẫu

- Là console application trên .NET Framework 4.7.2.
- Địa chỉ thiết bị, register và CRC đang hard-code.
- Sử dụng `Thread.Sleep`, có nguy cơ khóa luồng.
- Đọc dữ liệu dựa trên `BytesToRead` tại một thời điểm, chưa bảo đảm nhận đủ frame.
- Chưa kiểm tra CRC response đầy đủ.
- Chưa có hàng đợi request trên một tuyến RS485.
- Chưa xử lý toàn diện timeout, retry, frame lỗi và reconnect.
- Không phù hợp để dùng trực tiếp làm ứng dụng production.

Chương trình mẫu chỉ được dùng để đối chiếu giao thức và register. Phần mềm production phải được xây dựng trong solution mới.

## 4. Điều kiện bắt đầu project

Một công việc chỉ được tính vào tiến độ chính thức sau khi các điều kiện liên quan đã sẵn sàng.

### 4.1 Điều kiện kỹ thuật bắt buộc

- [ ] Xác nhận model controller thực tế có RS485.
- [ ] Xác nhận không dùng TK4W-R4RN nếu không có thiết bị chuyển đổi 4-20 mA sang hệ thống số.
- [ ] Có communication manual hoặc register map chính thức.
- [ ] Xác định register PV, SV, alarm, sensor error và vị trí dấu thập phân.
- [ ] Có ít nhất một controller TK Series RS485 thật.
- [ ] Có USB-RS485 hoặc gateway sẽ sử dụng tại hiện trường.
- [ ] Xác nhận cấu hình 9.600 bps, parity, stop bit, response waiting time và địa chỉ.
- [ ] Xác nhận số tuyến RS485 và số thiết bị trên từng tuyến.

### 4.2 Điều kiện nghiệp vụ bắt buộc

- [ ] Chốt danh sách 21 tank của phiên bản 1.
- [ ] Có mã tank, tên tank, vị trí và loại hóa chất.
- [ ] Ngưỡng High/Low từng tank đã được chủ quản công nghệ hoặc EHS xác nhận.
- [ ] Chốt hysteresis và delay alarm.
- [ ] Chốt chu kỳ polling và chu kỳ lưu dữ liệu.
- [ ] Chốt thời hạn lưu dữ liệu.
- [ ] Chốt quyền ghi SV: read-only hoặc cho phép ghi.
- [ ] Chốt định dạng báo cáo và dữ liệu cần xuất.
- [ ] Xác định người đại diện nghiệm thu UAT.

### 4.3 Điều kiện hạ tầng

- [ ] Xác định máy tính chạy phần mềm.
- [ ] Xác định phiên bản Windows và độ phân giải màn hình.
- [ ] Có quyền cài đặt phần mềm và driver thiết bị.
- [ ] IT Samsung xác nhận chính sách antivirus, Windows Update và tự khởi động ứng dụng.
- [ ] Đồng hồ hệ thống được đồng bộ đúng múi giờ vận hành.
- [ ] Có thư mục được phép lưu database, log, backup và file export.

## 5. Nguyên tắc quản lý tiến độ

### 5.1 Trạng thái công việc

| Trạng thái | Ý nghĩa |
|---|---|
| Not Started | Chưa bắt đầu |
| Ready | Đã đủ điều kiện đầu vào |
| In Progress | Đang thực hiện |
| In Review | Đang review code hoặc kiểm thử |
| Blocked | Không thể tiếp tục vì thiếu đầu vào hoặc phụ thuộc |
| Done | Đáp ứng toàn bộ tiêu chí hoàn thành |

### 5.2 Definition of Ready

Một task chỉ được chuyển sang In Progress khi:

- Yêu cầu đã đủ rõ để triển khai.
- Đầu vào và phụ thuộc đã có.
- Tiêu chí nghiệm thu của task đã được xác định.
- Không còn quyết định kỹ thuật quan trọng chưa được chốt.
- Có dữ liệu hoặc thiết bị/simulator cần thiết để kiểm thử.

### 5.3 Definition of Done chung

Một task chỉ được đánh dấu Done khi:

- Code đã hoàn thành và build thành công.
- Không còn lỗi biên dịch.
- Đã review code.
- Unit test hoặc integration test liên quan chạy thành công.
- Không để lại hard-code không được phê duyệt.
- Xử lý lỗi và logging đã được bổ sung.
- Tài liệu hoặc configuration liên quan đã cập nhật.
- Sản phẩm đầu ra đáp ứng tiêu chí nghiệm thu của task.

## 6. Tổng quan các mốc

| Mốc | Nội dung | Thời điểm mục tiêu | Điều kiện qua mốc |
|---|---|---:|---|
| M0 | Chốt yêu cầu và đầu vào | Cuối ngày 2 | Tất cả quyết định P0 được xác nhận |
| M1 | Driver Modbus hoạt động | Cuối ngày 5 | Đọc đúng PV/SV từ controller hoặc simulator chuẩn |
| M2 | Nền tảng và database hoàn thành | Cuối ngày 7 | Solution, logging, cấu hình và SQLite hoạt động |
| M3 | Dashboard thời gian thực | Cuối ngày 10 | Hiển thị 21 tank và xử lý trạng thái kết nối |
| M4 | Alarm và historian hoàn thành | Cuối ngày 14 | Alarm không trùng, dữ liệu được lưu đúng |
| M5 | Tra cứu, báo cáo và quản trị | Cuối ngày 16 | Trend, export, user, audit và backup hoạt động |
| M6 | Hoàn thành kiểm thử tích hợp | Cuối ngày 18 | Các test lỗi truyền thông và dữ liệu đạt yêu cầu |
| M7 | Release Candidate | Cuối ngày 20 | Installer, tài liệu và UAT nội bộ hoàn thành |
| M8 | Nghiệm thu hiện trường | Tuần dự phòng | Chạy 72 giờ và UAT được ký xác nhận |

## 7. Lộ trình chi tiết 20 ngày làm việc

### Tuần 1 Xác nhận giao thức và xây dựng driver

#### Ngày 1 Chốt phạm vi phiên bản 1

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| R01 | Rà soát tài liệu yêu cầu | Danh sách chức năng P0, P1, P2 | Không còn chức năng P0 mô tả mơ hồ |
| R02 | Chốt số lượng tank | Danh sách 21 tank | Mỗi tank có mã, tên, hóa chất và vị trí |
| R03 | Chốt phạm vi read/write | Biên bản quyết định | Xác định rõ chỉ đọc hay được ghi SV |
| R04 | Chốt ngưỡng alarm | Bảng ngưỡng | Người phụ trách công nghệ/EHS xác nhận |
| R05 | Chốt topology RS485 | Sơ đồ một hoặc nhiều tuyến | Có số thiết bị và địa chỉ trên từng tuyến |

**Cổng nghiệm thu ngày 1:** phạm vi MVP và production được tách rõ; các nội dung chưa chốt được ghi thành blocker có người phụ trách.

#### Ngày 2 Xác nhận register và thiết bị

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| R06 | Đối chiếu manual và sample Autonics | Register map bản đầu | Có PV, SV, model, alarm và sensor error nếu hỗ trợ |
| R07 | Xác nhận scale dữ liệu | Quy tắc chuyển đổi | Đọc đúng số âm và vị trí thập phân |
| R08 | Xác nhận thông số serial | Connection profile | Baud, parity, stop bits, timeout và address rõ ràng |
| R09 | Kiểm tra controller thật | Biên bản kết nối | Controller phản hồi đúng Modbus RTU |
| R10 | Tạo test vector CRC/frame | Bộ dữ liệu test | Có request/response hợp lệ và frame lỗi |

**Cổng M0:** register chưa được xác nhận thì phần driver chỉ được phát triển với simulator, không được coi là nghiệm thu thiết bị thật.

#### Ngày 3 Tạo solution và khung driver

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| A01 | Tạo solution .NET 10 | Solution nhiều project | Build Debug và Release thành công |
| A02 | Tạo domain model | Tank, Device, Connection, Sample, Alarm | Không phụ thuộc trực tiếp vào WinForms |
| A03 | Tạo Modbus request builder | Frame request | CRC và byte order đúng test vector |
| A04 | Tạo response parser | Parsed response | Phát hiện CRC lỗi, exception và frame thiếu |
| A05 | Tạo register catalog | Register constants/metadata | Không hard-code register trong Form |

#### Ngày 4 Serial transport và simulator

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| C01 | Xây dựng SerialPort transport | Async transport | Không khóa UI thread |
| C02 | Xây dựng request queue | Một request tại một thời điểm mỗi tuyến | Không chồng frame trên RS485 |
| C03 | Xử lý timeout và retry | Retry policy | Timeout được phân loại và log đúng |
| C04 | Xử lý frame tích lũy | Frame reader | Nhận được response bị chia thành nhiều lần đọc |
| C05 | Tạo simulator | Virtual devices | Mô phỏng 21–40 địa chỉ, PV, SV và lỗi |

#### Ngày 5 Kiểm thử driver

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| C06 | Đọc model name | Kết quả model | Khớp thiết bị thật hoặc mẫu chuẩn |
| C07 | Đọc PV | Giá trị PV | Khớp màn hình controller trong độ phân giải hiển thị |
| C08 | Đọc SV | Giá trị SV | Khớp màn hình controller |
| C09 | Đọc trạng thái alarm | Alarm status | Khớp trạng thái thực hoặc simulator |
| C10 | Test mất kết nối | Kết quả timeout/reconnect | Không crash và kết nối lại được |
| C11 | Unit test driver | Bộ test tự động | Tất cả test CRC/parser/state chạy thành công |

**Cổng M1:** driver phải chạy liên tục tối thiểu 4 giờ với simulator hoặc controller thật mà không treo và không rò rỉ request.

### Tuần 2 Nền tảng dữ liệu và dashboard

#### Ngày 6 Database và configuration

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| D01 | Thiết kế schema SQLite | Schema/migration | Có bảng Connections, Devices, Tanks, Samples, Alarms và AuditLogs |
| D02 | Repository và transaction | Persistence layer | Insert/query hoạt động và có xử lý lỗi |
| D03 | App configuration | File/database settings | Cấu hình không hard-code trong executable |
| D04 | Logging | Log file xoay vòng | Có mức Info, Warning, Error và không log mật khẩu |

#### Ngày 7 Connection manager và polling service

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| P01 | Quản lý nhiều connection | Connection manager | Một tuyến lỗi không dừng tuyến khác |
| P02 | Polling scheduler | Background polling | Chu kỳ cấu hình được, không khóa UI |
| P03 | Device state machine | Online/Offline/Error | Chuyển trạng thái nhất quán |
| P04 | Retry và reconnect | Recovery mechanism | Tự phục hồi khi kết nối trở lại |
| P05 | Data quality | Good/Bad/Unknown | Không coi dữ liệu cũ là dữ liệu mới |

**Cổng M2:** khởi động lại ứng dụng phải phục hồi được cấu hình và bắt đầu polling mà không phải nhập lại toàn bộ hệ thống.

#### Ngày 8 Thiết kế giao diện tổng thể

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| U01 | Main shell | Form chính | Có navigation, status bar và khu vực alarm |
| U02 | Tank card component | UserControl tái sử dụng | Không tạo Form riêng cho từng tank |
| U03 | Layout 21 tank | Dashboard layout | Đọc rõ trên màn hình 1920 x 1080 |
| U04 | Layout mở rộng 40 tank | Group/paging | Không phải sửa kiến trúc UI khi thêm tank |
| U05 | Theme trạng thái | Màu và icon | Normal, High, Low, Sensor Error, Offline phân biệt rõ |

#### Ngày 9 Kết nối UI với dữ liệu thời gian thực

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| U06 | Binding trạng thái tank | Dashboard realtime | UI cập nhật qua cơ chế thread-safe |
| U07 | Hiển thị PV/SV | Giá trị và đơn vị | Scale và timestamp đúng |
| U08 | Hiển thị kết nối | Online/Offline indicator | Phản ánh đúng device state |
| U09 | Filter/group | Bộ lọc dashboard | Lọc được theo khu vực và hóa chất |
| U10 | Fullscreen | Chế độ vận hành | Hoạt động đúng trên màn hình mục tiêu |

#### Ngày 10 Kiểm thử dashboard và tải

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| U11 | Test 21 thiết bị | Báo cáo test | Tất cả card cập nhật trong chu kỳ yêu cầu |
| U12 | Test 40 thiết bị/two buses | Báo cáo mở rộng | Hai tuyến hoạt động độc lập |
| U13 | Test resize/DPI | Báo cáo UI | Không cắt chữ, chồng nội dung hoặc mất nút |
| U14 | Test timeout hàng loạt | Báo cáo lỗi | UI không treo khi nhiều thiết bị offline |

**Cổng M3:** dashboard phải hiển thị đúng 21 tank và duy trì khả năng thao tác trong khi polling liên tục.

### Tuần 3 Alarm, historian và báo cáo

#### Ngày 11 Alarm state machine

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| AL01 | High Alarm | Rule/state | Tạo và kết thúc đúng theo threshold/hysteresis |
| AL02 | Low Alarm | Rule/state | Tạo và kết thúc đúng theo threshold/hysteresis |
| AL03 | Sensor Error | Rule/state | Giá trị lỗi không được lưu như PV hợp lệ |
| AL04 | Communication Error | Rule/state | Kích hoạt sau số retry quy định |
| AL05 | Communication Restored | Recovery event | Ghi nhận đúng một lần khi kết nối phục hồi |

#### Ngày 12 Alarm UI và acknowledge

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| AL06 | Active alarm panel | Danh sách alarm | Hiển thị theo mức ưu tiên và thời gian |
| AL07 | Âm thanh | Alarm sound | Hoạt động khi có alarm mới |
| AL08 | Acknowledge | Thao tác xác nhận | Không xóa điều kiện alarm đang tồn tại |
| AL09 | Alarm history | Database records | Không tạo bản ghi lặp mỗi chu kỳ polling |
| AL10 | Khôi phục sau restart | State recovery | Không mất hoặc nhân đôi sự kiện |

#### Ngày 13 Historian và retention

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| H01 | Lưu mẫu định kỳ | TemperatureSamples | Đúng tank, timestamp, PV, SV và quality |
| H02 | Lưu theo sự kiện | Event samples | Lưu ngay khi có alarm hoặc phục hồi |
| H03 | Retention policy | Job dọn dữ liệu | Không xóa dữ liệu ngoài chính sách |
| H04 | Index database | Query indexes | Truy vấn theo tank và thời gian đáp ứng mục tiêu |

#### Ngày 14 Lịch sử và biểu đồ

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| H05 | Lịch sử nhiệt độ | Data grid | Lọc theo tank và khoảng thời gian |
| H06 | Lịch sử alarm | Alarm grid | Lọc theo loại và trạng thái xác nhận |
| H07 | Trend chart | Biểu đồ | Có PV, High/Low limit và khoảng mất dữ liệu |
| H08 | Export CSV/Excel | File báo cáo | Nội dung khớp dữ liệu trên màn hình |

**Cổng M4:** test High, Low, Sensor Error và Offline phải tạo đúng vòng đời alarm, không trùng và không mất dữ liệu.

#### Ngày 15 Cấu hình vận hành

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| CFG01 | Quản lý tank | CRUD screen | Không cho trùng mã tank |
| CFG02 | Quản lý device/address | Device screen | Không cho trùng address trên cùng tuyến |
| CFG03 | Cấu hình threshold | Alarm settings | Validate Low nhỏ hơn High |
| CFG04 | Cấu hình polling/storage | System settings | Giá trị ngoài phạm vi bị từ chối |
| CFG05 | Test connection | Diagnostic action | Hiển thị kết quả rõ ràng và có log |

### Tuần 4 Bảo mật, kiểm thử và phát hành

#### Ngày 16 Người dùng, audit và backup

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| SEC01 | Operator/Admin roles | Phân quyền | Operator không sửa cấu hình quan trọng |
| SEC02 | Password storage | Authentication | Không lưu mật khẩu dạng rõ |
| SEC03 | Audit log | Nhật ký thao tác | Ghi thay đổi threshold/config/acknowledge |
| BK01 | Backup | File backup | Tạo được khi hệ thống đang vận hành an toàn |
| BK02 | Restore | Quy trình restore | Khôi phục được trên database kiểm thử |

**Cổng M5:** backup phải được kiểm tra restore thực tế; chỉ tạo được file backup chưa được coi là hoàn thành.

#### Ngày 17 Kiểm thử tích hợp và fault injection

| Mã | Công việc | Điều kiện hoàn thành |
|---|---|---|
| T01 | Rút/cắm USB-RS485 | Ứng dụng không crash và reconnect được |
| T02 | Controller mất nguồn | Tank chuyển Offline và các tank khác tiếp tục cập nhật |
| T03 | CRC lỗi | Frame bị từ chối và được log đúng |
| T04 | Response thiếu/chậm | Timeout/retry hoạt động đúng |
| T05 | Trùng Modbus address | Phần mềm cảnh báo cấu hình |
| T06 | Database tạm khóa | Không làm mất ổn định toàn ứng dụng |
| T07 | Khởi động lại Windows | Ứng dụng tự chạy và tiếp tục vận hành |

#### Ngày 18 Kiểm thử dữ liệu và hiệu năng

| Mã | Công việc | Điều kiện hoàn thành |
|---|---|---|
| T08 | Test polling 21 tank | Chu kỳ cập nhật đáp ứng cấu hình đã chốt |
| T09 | Test 40 tank/two buses | Không ảnh hưởng lẫn nhau giữa hai tuyến |
| T10 | Query một tháng dữ liệu | Mục tiêu dưới 3 giây trên máy vận hành |
| T11 | Export dữ liệu lớn | Không treo UI, file hợp lệ |
| T12 | Restart recovery | Không tạo alarm hoặc sample trùng bất hợp lý |
| T13 | So sánh PV/SV | Khớp màn hình controller |

**Cổng M6:** không còn lỗi Critical/Blocker; tất cả test P0 phải đạt.

#### Ngày 19 Đóng gói và tài liệu

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| DEP01 | Build Release x64 | Release package | Build sạch và chạy trên máy không có Visual Studio |
| DEP02 | Installer | Bộ cài đặt | Cài mới, nâng cấp và gỡ cài đặt được |
| DEP03 | Tài liệu cài đặt | Installation guide | IT có thể thực hiện theo tài liệu |
| DEP04 | Tài liệu vận hành | User guide | Operator thực hiện được thao tác chính |
| DEP05 | Tài liệu xử lý lỗi | Troubleshooting guide | Có hướng dẫn COM, offline, backup và log |
| DEP06 | Release notes | Danh sách thay đổi | Có version, ngày phát hành và known issues |

#### Ngày 20 UAT nội bộ và Release Candidate

| Mã | Công việc | Điều kiện hoàn thành |
|---|---|---|
| UAT01 | Demo quy trình vận hành | Người dùng thực hiện được dashboard, acknowledge và tra cứu |
| UAT02 | Chạy bộ test nghiệm thu | Tất cả P0 đạt, không còn blocker |
| UAT03 | Ghi nhận phản hồi | Mọi thay đổi được phân loại bug hoặc change request |
| UAT04 | Khóa phạm vi release | Không thêm chức năng mới vào RC |
| UAT05 | Tạo Release Candidate | Có installer, source, tài liệu và checksum |

**Cổng M7:** Release Candidate chỉ được phát hành hiện trường khi không còn lỗi Critical hoặc High ảnh hưởng vận hành.

## 8. Tuần dự phòng và nghiệm thu hiện trường

### Ngày 21–22 Cài đặt và SAT

- Cài đặt trên máy giám sát thực tế.
- Cấu hình COM port/gateway thực tế.
- Import danh sách tank.
- Kiểm tra lần lượt từng địa chỉ Modbus.
- So sánh PV/SV giữa phần mềm và controller.
- Mô phỏng ít nhất một High, Low, Sensor Error và Offline.
- Kiểm tra đường dẫn backup và export.

**Tiêu chí hoàn thành:** tất cả 21 tank online hoặc có biên bản ghi rõ điểm chưa sẵn sàng do phần cứng; dữ liệu hợp lệ không bị gán nhầm tank.

### Ngày 23–25 Chạy ổn định và UAT

- Chạy liên tục tối thiểu 72 giờ.
- Theo dõi timeout, reconnect, dung lượng database và log.
- Kiểm tra alarm thực tế và lịch sử.
- Sửa lỗi production nếu phát sinh.
- Đào tạo operator và administrator.
- Ký biên bản UAT và bàn giao.

**Cổng M8:** đáp ứng toàn bộ Definition of Done cấp project trong phần 10.

## 9. Tiêu chí nghiệm thu định lượng

| Nhóm | Tiêu chí |
|---|---|
| Build | Build Release thành công, không có lỗi biên dịch |
| Driver | CRC, frame parser và exception handling vượt toàn bộ test |
| Độ chính xác | PV/SV khớp giá trị hiển thị trên controller theo độ phân giải thiết bị |
| Polling | 21 tank được cập nhật trong chu kỳ đã thống nhất |
| Mở rộng | Simulator hoặc hệ thống test chạy được 40 tank trên ít nhất hai connection |
| UI | Không treo khi nhiều thiết bị timeout; hiển thị rõ ở 1920 x 1080 |
| Reconnect | Tự kết nối lại sau khi đường truyền được phục hồi, mục tiêu không quá 30 giây |
| Alarm | Không tạo alarm trùng; lưu đúng start, end và acknowledge |
| Dữ liệu | Không lưu giá trị lỗi như PV hợp lệ; timestamp và tank mapping chính xác |
| Query | Truy vấn một tháng của một tank mục tiêu dưới 3 giây |
| Export | File mở được và khớp dữ liệu được lọc |
| Backup | Backup được restore thành công trên môi trường kiểm thử |
| Ổn định | Chạy liên tục tối thiểu 72 giờ không crash hoặc mất dữ liệu nghiêm trọng |
| Bảo mật | Operator không thay đổi được cấu hình quản trị |
| Triển khai | Cài đặt được trên máy không có Visual Studio |
| UAT | Không còn lỗi Critical/High và có xác nhận của đại diện vận hành |

## 10. Definition of Done cấp project

Project chỉ được coi là hoàn thành khi tất cả điều kiện sau được đáp ứng:

### 10.1 Chức năng

- [ ] Dashboard hiển thị đúng toàn bộ 21 tank.
- [ ] Hỗ trợ cấu hình mở rộng tối thiểu 40 tank.
- [ ] Đọc đúng PV và SV theo phạm vi đã chốt.
- [ ] High, Low, Sensor Error và Communication Error hoạt động đúng.
- [ ] Acknowledge và lịch sử alarm hoạt động đúng.
- [ ] Lưu lịch sử nhiệt độ đúng chu kỳ.
- [ ] Tra cứu, biểu đồ và export hoạt động đúng.
- [ ] Cấu hình tank, device và connection được validate.
- [ ] Backup và restore đã được kiểm thử.
- [ ] Audit log ghi nhận đúng thao tác quan trọng.

### 10.2 Độ ổn định

- [ ] Không crash khi mất hoặc phục hồi RS485.
- [ ] Một thiết bị lỗi không làm dừng thiết bị khác.
- [ ] Một tuyến lỗi không làm dừng tuyến khác.
- [ ] Không treo UI khi polling hoặc export.
- [ ] Chạy liên tục tối thiểu 72 giờ đạt yêu cầu.

### 10.3 Kiểm thử

- [ ] Unit test P0 chạy thành công.
- [ ] Integration test với controller thật hoàn thành.
- [ ] Test 21 tank hoàn thành.
- [ ] Test mở rộng 40 tank bằng simulator hoặc phần cứng hoàn thành.
- [ ] Fault injection test hoàn thành.
- [ ] Không còn lỗi Critical hoặc High.

### 10.4 Bàn giao

- [ ] Source code và solution đã bàn giao.
- [ ] Database schema/migration đã bàn giao.
- [ ] Installer Release x64 đã bàn giao.
- [ ] Configuration mẫu 21 tank đã bàn giao.
- [ ] Simulator/công cụ test đã bàn giao.
- [ ] Tài liệu cài đặt đã bàn giao.
- [ ] Tài liệu vận hành đã bàn giao.
- [ ] Tài liệu backup/restore đã bàn giao.
- [ ] Danh sách dependency và license đã bàn giao.
- [ ] Operator và Administrator đã được đào tạo.
- [ ] Biên bản UAT đã được ký xác nhận.

## 11. Danh sách sản phẩm bàn giao

| Sản phẩm | Nội dung tối thiểu |
|---|---|
| Source code | Toàn bộ C# source, solution và project files |
| Release package | Executable, dependencies và configuration mặc định |
| Installer | Cài mới, nâng cấp và gỡ cài đặt |
| Database | Schema, migration và database mẫu |
| Device configuration | Danh sách tank, connection và Modbus address |
| Test tools | Simulator hoặc chương trình kiểm tra Modbus |
| Test report | Unit, integration, SAT, soak test và UAT |
| Installation guide | Yêu cầu máy, cài đặt, cấu hình và nâng cấp |
| User guide | Dashboard, alarm, lịch sử, export và acknowledge |
| Admin guide | Device, threshold, account, backup và restore |
| Troubleshooting | COM port, timeout, offline, log và phục hồi |
| Release notes | Version, thay đổi, giới hạn và known issues |

## 12. Phân công trách nhiệm đề xuất

| Vai trò | Trách nhiệm |
|---|---|
| Chủ project/Product Owner | Chốt phạm vi, ưu tiên và phê duyệt thay đổi |
| Lập trình viên WinForms | Kiến trúc, code, test, đóng gói và tài liệu kỹ thuật |
| Kỹ thuật tự động hóa | Register map, controller, RS485 và kiểm thử thiết bị |
| Vận hành | Xác nhận UI, alarm workflow và thực hiện UAT |
| Công nghệ/EHS | Phê duyệt ngưỡng nhiệt độ và quy tắc alarm |
| IT Samsung | Máy tính, quyền cài đặt, antivirus, backup và chính sách bảo mật |
| QA/Test | Quản lý test case, lỗi và biên bản nghiệm thu |

## 13. Rủi ro và phương án dự phòng

| Rủi ro | Tác động | Phương án |
|---|---|---|
| Controller không có RS485 | Không thể dùng driver Modbus trực tiếp | Xác nhận model; thay controller hoặc bổ sung gateway phù hợp |
| Thiếu register map | Không xác nhận được dữ liệu | Lấy manual/sample chính thức; không phát hành dựa trên register suy đoán |
| Không có thiết bị thật | Không nghiệm thu được driver | Phát triển bằng simulator nhưng dừng cổng nghiệm thu M1/M6 |
| Nhiễu RS485 | Timeout và dữ liệu không ổn định | Logging, retry, kiểm tra topology/cáp/termination với kỹ thuật điện |
| Trùng Modbus address | Dữ liệu sai hoặc xung đột | Validate cấu hình và kiểm tra từng controller |
| Thay đổi 21 thành 40 tank muộn | Trễ UI và truyền thông | Hỗ trợ multi-connection và dynamic dashboard từ đầu |
| Thay đổi ngưỡng alarm | Phải sửa và test lại nghiệp vụ | Lưu threshold trong cấu hình/database, có audit |
| Yêu cầu ghi SV phát sinh | Tăng rủi ro vận hành | Tách change request, thêm phân quyền và xác nhận hai bước |
| Yêu cầu remote access | Tăng phạm vi và bảo mật | Tách giai đoạn 2, cần IT phê duyệt |
| Antivirus chặn ứng dụng | Không triển khai được | Kiểm thử installer sớm trên môi trường IT cung cấp |
| Database tăng nhanh | Chậm truy vấn/đầy ổ | Retention, index, backup và cảnh báo dung lượng |

## 14. Quy tắc xử lý blocker và thay đổi

- Blocker phải ghi rõ nguyên nhân, người phụ trách và ngày cần phản hồi.
- Thời gian bị chặn do thiếu controller, register map hoặc quyền IT không tính vào 20 ngày phát triển.
- Bug làm sai dữ liệu, mất alarm hoặc crash được xếp mức Critical/High và phải sửa trước release.
- Yêu cầu mới ngoài phạm vi phiên bản 1 phải tạo change request.
- Remote access, ghi SV, tích hợp MES/SCADA và nhiều máy trạm là thay đổi phạm vi đáng kể.
- Sau ngày 20 chỉ sửa bug cho Release Candidate; không thêm tính năng mới nếu chưa đánh giá lại tiến độ.

## 15. Theo dõi tiến độ hàng ngày

Mỗi ngày cần cập nhật tối thiểu:

- Task đã hoàn thành.
- Task đang thực hiện.
- Test đã chạy và kết quả.
- Bug mới phát hiện.
- Blocker và người cần xử lý.
- Thay đổi phạm vi nếu có.
- Kế hoạch ngày tiếp theo.

Mẫu cập nhật:

```text
Ngày:
Hoàn thành:
-

Đang thực hiện:
-

Kiểm thử:
-

Blocker:
-

Kế hoạch tiếp theo:
-
```

## 16. Kết luận kế hoạch

Lộ trình mục tiêu gồm 20 ngày phát triển và 5 ngày dự phòng hiện trường. Driver Modbus và xác nhận dữ liệu thiết bị là đường găng của project. Dashboard, database và simulator có thể phát triển song song, nhưng phần mềm không được nghiệm thu nếu chưa kiểm thử trên controller thật.

Để giữ tiến độ 4 tuần, các đầu vào P0 gồm model controller, register map, thiết bị RS485, danh sách tank và ngưỡng alarm phải được cung cấp trong hai ngày đầu. Tuần dự phòng được dành cho SAT, chạy ổn định 72 giờ và xử lý các vấn đề chỉ xuất hiện tại hiện trường.
