# Lộ trình hoàn thiện project phần mềm giám sát nhiệt độ hóa chất SEMV

## 1. Thông tin kế hoạch

| Nội dung | Giá trị |
|---|---|
| Project | Phần mềm giám sát nhiệt độ hóa chất SEMV |
| Loại ứng dụng | Windows Forms desktop application |
| Nền tảng đề xuất | C# và .NET 10 LTS, Windows x64 |
| Phạm vi phiên bản 1 | Giám sát 21 tank, mở rộng được đến tối thiểu 40 tank |
| Giao tiếp | RS485 Modbus RTU với Autonics TK Series |
| Cơ sở dữ liệu | PostgreSQL; mặc định có thể chạy local dưới dạng Windows Service, hỗ trợ chuyển sang máy chủ tập trung |
| Thời gian phát triển mục tiêu | 25 ngày làm việc, chỉ bắt đầu sau khi M0 hoàn tất |
| SAT/UAT hiện trường | 5 ngày làm việc |
| Tổng thời gian kế hoạch | 5 tuần phát triển và 1 tuần SAT/UAT |
| Phương thức thực hiện | Một lập trình viên WinForms có AI hỗ trợ, phối hợp kỹ thuật tự động hóa và người vận hành |
| Ngày lập kế hoạch | 08/10/2026 |

M0 là giai đoạn chuẩn bị ngoài 25 ngày phát triển. Đồng hồ tiến độ chỉ bắt đầu khi register map, controller test, danh sách tank/ngưỡng, PostgreSQL, quyền IT, quyết định watchdog và dependency/license P0 đã sẵn sàng. Thời gian chờ các đầu vào này không được che giấu trong tuần SAT/UAT.

Tài liệu này triển khai công việc từ [Yêu cầu project phần mềm giám sát nhiệt độ hóa chất SEMV](./Yeu-cau-project-phan-mem-giam-sat-hoa-chat-SEMV.md) và cập nhật bằng chứng tại [Traceability Matrix SEMV](./Traceability-Matrix-SEMV.md). Tài liệu yêu cầu là nguồn chuẩn cho FR/NFR và Definition of Done; lộ trình không lặp lại hoặc tự thay đổi tiêu chí đó.

## 2. Mục tiêu hoàn thành

Sau khi kết thúc lộ trình, project phải có một bản phần mềm WinForms có thể cài đặt trên máy giám sát và thực hiện được các chức năng sau:

- Kết nối ổn định với các bộ điều khiển Autonics TK Series qua RS485 Modbus RTU.
- Đọc và hiển thị nhiệt độ PV của 21 tank theo thời gian thực.
- Đọc SV và trạng thái alarm nếu register map/model thiết bị hỗ trợ.
- Nhận biết High Alarm, Low Alarm, Sensor Error và Communication Error.
- Phân biệt SoftwareAlarm, ControllerAlarm, LocalHardwareAlarm và AlarmMismatch.
- Lưu lịch sử nhiệt độ, alarm và lỗi truyền thông.
- Hiển thị biểu đồ, tra cứu và xuất dữ liệu.
- Quản lý cấu hình tank, kết nối và ngưỡng cảnh báo.
- Phân quyền người dùng cơ bản, audit log, backup và restore.
- Maintenance Mode có thời hạn, alarm reminder không tạo bản ghi trùng và nhận diện đúng người acknowledge.
- Tự động kết nối lại sau sự cố và tiếp tục hoạt động khi một thiết bị bị lỗi.
- Có installer, tài liệu vận hành, mã nguồn và biên bản UAT.
- Kiến trúc hỗ trợ tối thiểu 40 tank và nhiều tuyến RS485.
- Có watchdog/heartbeat, durable queue 24 giờ và capacity test PostgreSQL 12 tháng/40 tank.

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

## 4. M0 — điều kiện trước khi bắt đầu 25 ngày phát triển

Một công việc chỉ được tính vào tiến độ chính thức sau khi các điều kiện liên quan đã sẵn sàng.

### 4.1 Điều kiện kỹ thuật bắt buộc

- [ ] Xác nhận model controller thực tế có RS485.
- [ ] Xác nhận không dùng TK4W-R4RN nếu không có thiết bị chuyển đổi 4-20 mA sang hệ thống số.
- [ ] Có communication manual hoặc register map chính thức.
- [ ] Xác định register PV, SV, alarm, sensor error và vị trí dấu thập phân.
- [ ] Bảng register ghi đủ function code, address, length, signed/unsigned, scale, decimal, unit, bit mask, giá trị lỗi và nguồn tài liệu.
- [ ] Cấu hình AL1/AL2 đã được xác nhận; không mặc định AL1 là High và AL2 là Low.
- [ ] Có ít nhất một controller TK Series RS485 thật.
- [ ] Có USB-RS485 hoặc gateway sẽ sử dụng tại hiện trường.
- [ ] Xác nhận cấu hình 9.600 bps, parity, stop bit, response waiting time và địa chỉ.
- [ ] Xác nhận số tuyến RS485 và số thiết bị trên từng tuyến.
- [ ] Có test vector cho giá trị âm, decimal, sensor error, CRC lỗi, frame thiếu và Modbus exception.

### 4.2 Điều kiện nghiệp vụ bắt buộc

- [ ] Chốt danh sách 21 tank của phiên bản 1.
- [ ] Có mã tank, tên tank, vị trí và loại hóa chất.
- [ ] Ngưỡng High/Low từng tank đã được chủ quản công nghệ hoặc EHS xác nhận.
- [ ] Chốt hysteresis và delay alarm.
- [ ] Chốt chu kỳ polling và chu kỳ lưu dữ liệu.
- [ ] Chốt thời hạn lưu dữ liệu.
- [ ] Chốt quyền ghi SV: read-only hoặc cho phép ghi.
- [ ] Chốt định dạng báo cáo và dữ liệu cần xuất.
- [ ] CSV được xác nhận là bắt buộc; Excel chỉ là P1 khi giấy phép thư viện được duyệt.
- [ ] Chốt cơ chế Operator: tài khoản ứng dụng + PIN hoặc Windows identity; không dùng tài khoản chung nếu cần audit.
- [ ] Chốt alarm reminder, Maintenance Mode và quy tắc AlarmMismatch.
- [ ] Xác định người đại diện nghiệm thu UAT.

### 4.3 Điều kiện hạ tầng

- [ ] Xác định máy tính chạy phần mềm.
- [ ] Xác định phiên bản Windows và độ phân giải màn hình.
- [ ] Có quyền cài đặt phần mềm và driver thiết bị.
- [ ] IT Samsung xác nhận chính sách antivirus, Windows Update và tự khởi động ứng dụng.
- [ ] Đồng hồ hệ thống được đồng bộ đúng múi giờ vận hành.
- [ ] Chốt PostgreSQL chạy trên máy giám sát hay máy chủ tập trung.
- [ ] Chốt phiên bản PostgreSQL, host, port, database name và phương thức TLS.
- [ ] Có tài khoản dịch vụ PostgreSQL theo nguyên tắc quyền tối thiểu; không dùng tài khoản `postgres` cho ứng dụng.
- [ ] Firewall/`pg_hba.conf` chỉ cho phép đúng máy ứng dụng kết nối và đã được IT phê duyệt.
- [ ] Có nơi được phép lưu hàng đợi cục bộ, log, backup và file export.
- [ ] Chốt lịch backup, thời hạn lưu backup, dung lượng dự kiến và người chịu trách nhiệm kiểm tra restore.
- [ ] Chốt phiên bản 1 dùng WinForms background service + Task Scheduler/watchdog; nếu yêu cầu chạy khi chưa đăng nhập thì phê duyệt thêm phạm vi Windows Service.
- [ ] Hoàn tất review dependency/license cho Npgsql, Dapper/repository, Modbus, chart, CSV/Excel, logging và installer.
- [ ] `.gitignore` đã loại trừ Office/PDF riêng tư, secret, `.vs`, `bin`, `obj`, log, backup, export và runtime data; generated files cũ đã được bỏ khỏi Git index.

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
| M0 | Chốt yêu cầu và đầu vào | Trước ngày phát triển 1 | Toàn bộ checklist M0 đạt hoặc có phê duyệt blocker/change request |
| M1 | Driver Modbus đã xác nhận | Cuối ngày 5 | Signed/scale đúng; block/single read và offline backoff đạt test controller/simulator |
| M2 | Nền tảng dữ liệu và polling | Cuối ngày 10 | PostgreSQL, queue 24 giờ, multi-bus polling và dashboard 21 tank hoạt động |
| M3 | Alarm và historian | Cuối ngày 15 | Alarm sources/mismatch, maintenance, reminder và historian không trùng |
| M4 | Quản trị và độ bền | Cuối ngày 20 | User/audit, backup/restore, watchdog, dependency và fault injection đạt |
| M5 | Release Candidate | Cuối ngày 25 | Capacity/regression/soak nội bộ, installer, tài liệu và traceability hoàn thành |
| M6 | Nghiệm thu hiện trường | Cuối ngày 30 | SAT, chạy 72 giờ, đào tạo và UAT được ký xác nhận |

## 7. Lộ trình chi tiết 25 ngày làm việc

### Tuần 1 Driver Modbus và bằng chứng giao thức

#### Ngày 1 Solution, register catalog và test vector

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| A01 | Tạo solution .NET 10 nhiều lớp | Solution build Debug/Release | Domain/Application/Infrastructure/WinForms không phụ thuộc ngược |
| A02 | Tạo register catalog có version | Metadata theo model/firmware | Có function, address, length, signed, scale, unit, bit mask và nguồn |
| A03 | Tạo test vector | Request/response chuẩn và lỗi | Bao phủ số âm, decimal, sensor error, CRC lỗi, frame thiếu và exception |
| A04 | Tạo parser signed/scale | Unit tests | PV/SV âm và dấu thập phân khớp vector đã duyệt |
| A05 | Cập nhật traceability | Dòng M1 trong matrix | Mỗi yêu cầu driver có task/test/bằng chứng dự kiến |

#### Ngày 2 Serial transport và frame parser

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| C01 | SerialPort transport bất đồng bộ | Transport layer | Không gọi blocking trên UI thread |
| C02 | Request queue từng tuyến | Một request in-flight/bus | Không chồng frame trên RS485 |
| C03 | Frame accumulator | Parser theo độ dài/CRC | Nhận đúng response bị chia nhỏ hoặc đến chậm |
| C04 | Timeout/Modbus exception | Error taxonomy | Timeout, bad CRC, exception và cancellation được phân loại |
| C05 | Log kỹ thuật có giới hạn | Diagnostic log | Không log secret, raw frame mặc định tắt và có rotation |

#### Ngày 3 Simulator và fault injection

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| C06 | Simulator 21–40 địa chỉ | Virtual devices | PV/SV/alarm thay đổi được theo kịch bản |
| C07 | Fault injection | Bộ lỗi mô phỏng | Có no response, delay, bad CRC, fragmented frame và exception |
| C08 | Giá trị biên | Bộ dữ liệu | Có negative value, sensor error, min/max và recovery |
| C09 | Multi-bus simulation | Ít nhất hai tuyến | Một tuyến lỗi không ảnh hưởng tuyến còn lại |

#### Ngày 4 Polling strategy và throughput spike

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| C10 | Single-register read | Kết quả benchmark | Đọc đúng Model/PV/SV/Status và đo chu kỳ 21 thiết bị |
| C11 | Block read `0x03E8–0x03EE` | Kết quả benchmark | Chỉ đạt khi manual/controller cho phép và ánh xạ đúng register |
| C12 | Chọn strategy + fallback | Biên bản kỹ thuật | Có số liệu đúng/sai/thời gian; fallback single read đã test |
| C13 | Online/offline scheduler | Backoff/probe | Online 1–2 giây; Offline probe 10–30 giây, retry giới hạn |
| C14 | Bus isolation | Scheduler nhiều tuyến | Thiết bị/tuyến Offline không chặn thiết bị/tuyến Online |

#### Ngày 5 Kiểm thử driver trên controller thật

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| C15 | Đọc Model/PV/SV/Status | Báo cáo đối chiếu | Khớp controller và register map được ký xác nhận |
| C16 | Test AL1/AL2/sensor error | Bảng ý nghĩa bit | Không suy đoán High/Low; ghi rõ cấu hình controller |
| C17 | Test giá trị âm/decimal | Bằng chứng ảnh/log | Không bị parse unsigned hoặc sai scale |
| C18 | Test timeout/reconnect/backoff | Báo cáo tải bus | Không crash, không retry dồn dập và tự phục hồi |
| C19 | Soak driver 4 giờ | Test report | Không treo, leak request hoặc trôi mapping thiết bị |

**Cổng M1:** không qua cổng nếu chưa có bằng chứng register/scale trên controller thật hoặc chưa có quyết định rõ phần nào tạm nghiệm thu bằng simulator.

### Tuần 2 Nền tảng dữ liệu và dashboard

#### Ngày 6 Database và configuration

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| D01 | Thiết kế schema PostgreSQL | Schema/migration | Có bảng Connections, Devices, Tanks, Samples, Alarms và AuditLogs; timestamp dùng `timestamptz` UTC |
| D02 | Migration/versioning | Bộ migration có số phiên bản | Tạo database trắng thành công; ứng dụng phát hiện schema sai phiên bản |
| D03 | Repository, transaction và pooling | Persistence layer dùng Npgsql | Insert/query hoạt động; dùng transaction ngắn và không mở connection riêng cho từng tank |
| D04 | Partition và index | Partition/index scripts | Samples phân vùng theo thời gian; có index `TankId + Timestamp` phục vụ tra cứu |
| D05 | Database role | Script role/quyền | Ứng dụng không dùng superuser và không có quyền ngoài phạm vi cần thiết |
| D06 | App configuration | File/database settings | Không hard-code host, port hoặc mật khẩu trong executable; secret được bảo vệ trên Windows |
| D07 | Logging và health check | Log xoay vòng/trạng thái DB | Có Info, Warning, Error; không log mật khẩu/connection string và hiển thị được trạng thái database |
| D08 | Database outage queue | Append-only/versioned local queue | Giữ tối thiểu 24 giờ, checksum/giới hạn dung lượng và replay idempotent |

#### Ngày 7 Connection manager và polling service

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| P01 | Quản lý nhiều connection | Connection manager | Một tuyến lỗi không dừng tuyến khác |
| P02 | Polling scheduler | Background polling | Chu kỳ cấu hình được, không khóa UI |
| P03 | Device state machine | Online/Offline/Error | Chuyển trạng thái nhất quán |
| P04 | Retry và reconnect | Recovery mechanism | Tự phục hồi khi kết nối trở lại |
| P05 | Data quality | Good/Bad/Unknown | Không coi dữ liệu cũ là dữ liệu mới |
| P06 | Durable database queue | Hàng đợi bền vững cục bộ | PostgreSQL mất kết nối không dừng polling; giữ tối thiểu 24 giờ và ghi bù không trùng |

**Checkpoint nền tảng:** khởi động lại ứng dụng phải phục hồi được cấu hình và bắt đầu polling mà không phải nhập lại toàn bộ hệ thống; cổng M2 được đánh giá cuối ngày 10 sau khi dashboard/queue hoàn thành.

#### Ngày 8 Thiết kế giao diện tổng thể

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| U01 | Main shell | Form chính | Có navigation, status bar và khu vực alarm |
| U02 | Tank card component | UserControl tái sử dụng | Không tạo Form riêng cho từng tank |
| U03 | Layout 21 tank | Dashboard layout | Đọc rõ trên màn hình 1920 x 1080 |
| U04 | Layout mở rộng 40 tank | Group/paging | Không phải sửa kiến trúc UI khi thêm tank |
| U05 | Theme trạng thái | Màu và icon | Normal, High, Low, Sensor Error, Offline phân biệt rõ |
| U05A | Health banner | Trạng thái hệ thống | Hiển thị rõ Database Degraded, queue backlog, bus lỗi và Maintenance Mode |

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
| U15 | Test offline backoff | Báo cáo chu kỳ quét | Thiết bị Online vẫn đạt chu kỳ khi nhiều thiết bị Offline |

**Cổng M2:** dashboard phải hiển thị đúng 21 tank, PostgreSQL/queue hoạt động và UI vẫn thao tác được khi polling hoặc database gián đoạn.

### Tuần 3 Alarm, historian và báo cáo

#### Ngày 11 Alarm state machine

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| AL01 | High Alarm | Rule/state | Tạo và kết thúc đúng theo threshold/hysteresis |
| AL02 | Low Alarm | Rule/state | Tạo và kết thúc đúng theo threshold/hysteresis |
| AL03 | Sensor Error | Rule/state | Giá trị lỗi không được lưu như PV hợp lệ |
| AL04 | Communication Error | Rule/state | Kích hoạt sau số retry quy định |
| AL05 | Communication Restored | Recovery event | Ghi nhận đúng một lần khi kết nối phục hồi |
| AL05A | Alarm source model | Domain/state | Lưu riêng Software/Controller/LocalHardware, không ghi đè lẫn nhau |
| AL05B | AlarmMismatch | Rule/state | Phát hiện nguồn không thống nhất sau delay, không suy đoán AL1/AL2 |

#### Ngày 12 Alarm UI và acknowledge

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| AL06 | Active alarm panel | Danh sách alarm | Hiển thị theo mức ưu tiên và thời gian |
| AL07 | Âm thanh | Alarm sound | Hoạt động khi có alarm mới |
| AL08 | Acknowledge | Thao tác xác nhận | Không xóa điều kiện alarm đang tồn tại |
| AL09 | Alarm history | Database records | Không tạo bản ghi lặp mỗi chu kỳ polling |
| AL10 | Khôi phục sau restart | State recovery | Không mất hoặc nhân đôi sự kiện |
| AL11 | Reminder chưa acknowledge | Timer/UI/sound | Nhắc lại theo cấu hình nhưng không tạo AlarmEvent mới |
| AL12 | Operator identity | Ack audit | Acknowledge gắn đúng user hiện tại, không dùng danh tính chung |

#### Ngày 13 Historian và retention

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| H01 | Lưu mẫu định kỳ | TemperatureSamples | Đúng tank, timestamp, PV, SV và quality |
| H02 | Lưu theo sự kiện | Event samples | Lưu ngay khi có alarm hoặc phục hồi |
| H03 | Retention policy | Job dọn dữ liệu | Không xóa dữ liệu ngoài chính sách |
| H04 | Partition/index PostgreSQL | Query plan và indexes | Truy vấn theo tank và thời gian đáp ứng mục tiêu, dọn partition không khóa hệ thống kéo dài |

#### Ngày 14 Lịch sử và biểu đồ

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| H05 | Lịch sử nhiệt độ | Data grid | Lọc theo tank và khoảng thời gian |
| H06 | Lịch sử alarm | Alarm grid | Lọc theo loại và trạng thái xác nhận |
| H07 | Trend chart | Biểu đồ | Có PV, High/Low limit và khoảng mất dữ liệu |
| H08 | Export CSV | File báo cáo bắt buộc | Mở được không cần Excel, nội dung khớp dữ liệu trên màn hình |
| H09 | Export Excel có điều kiện | File `.xlsx` P1 | Chỉ Done khi thư viện/license được duyệt; không chặn RC nếu CSV đạt và Excel ngoài phạm vi |

**Cổng giữa tuần:** High, Low, Sensor Error, Offline, source mismatch và reminder phải có vòng đời đúng, không trùng và không mất dữ liệu.

#### Ngày 15 Cấu hình vận hành

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| CFG01 | Quản lý tank | CRUD screen | Không cho trùng mã tank |
| CFG02 | Quản lý device/address | Device screen | Không cho trùng address trên cùng tuyến |
| CFG03 | Cấu hình threshold | Alarm settings | Validate Low nhỏ hơn High |
| CFG04 | Cấu hình polling/storage | System settings | Giá trị ngoài phạm vi bị từ chối |
| CFG05 | Test connection | Diagnostic action | Hiển thị kết quả rõ ràng và có log |
| CFG06 | Maintenance Mode | UI + state + audit | Có phạm vi, lý do, user, start/end, auto-expiry và hiển thị rõ |
| CFG07 | Maintenance policy | Suppress rules | Không xóa alarm; polling/lịch sử tiếp tục trừ khi dừng tuyến có chủ ý |

**Cổng M3:** alarm sources/mismatch, maintenance, reminder, historian và CSV đáp ứng các test P0/P1 tương ứng trong traceability matrix.

### Tuần 4 Bảo mật, độ bền và kiểm thử tích hợp

#### Ngày 16 Người dùng, audit và backup

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| SEC01 | Operator/Admin roles | Phân quyền | Operator không sửa cấu hình quan trọng |
| SEC02 | Password storage | Authentication | Không lưu mật khẩu dạng rõ |
| SEC03 | Audit log | Nhật ký thao tác | Ghi thay đổi threshold/config/acknowledge |
| SEC04 | Quick user switch | User + PIN/Windows identity | Đổi Operator nhanh, acknowledge gắn đúng người |
| SEC05 | Session timeout | Auto lock | Khóa thao tác quản trị nhưng dashboard/polling vẫn chạy |
| BK01 | Backup PostgreSQL | File do `pg_dump` tạo | Backup có schema, dữ liệu và phiên bản migration; không lộ mật khẩu trong log |
| BK02 | Restore PostgreSQL | Quy trình `pg_restore` | Khôi phục được vào database kiểm thử riêng và ứng dụng kết nối/tra cứu bình thường |
| BK03 | Backup schedule/retention | Lịch và chính sách lưu | Tự động chạy theo lịch, có log kết quả và cảnh báo khi thất bại |

**Cổng backup:** backup phải được restore thực tế; chỉ tạo được file backup chưa được coi là hoàn thành.

#### Ngày 17 Kiểm thử tích hợp và fault injection

| Mã | Công việc | Điều kiện hoàn thành |
|---|---|---|
| T01 | Rút/cắm USB-RS485 | Ứng dụng không crash và reconnect được |
| T02 | Controller mất nguồn | Tank chuyển Offline và các tank khác tiếp tục cập nhật |
| T03 | CRC lỗi | Frame bị từ chối và được log đúng |
| T04 | Response thiếu/chậm | Timeout/retry hoạt động đúng |
| T05 | Trùng Modbus address | Phần mềm cảnh báo cấu hình |
| T06 | PostgreSQL service restart/mất mạng | Polling tiếp tục, UI báo Degraded, dữ liệu vào queue và được ghi bù không trùng sau phục hồi |
| T07 | Khởi động lại Windows | Ứng dụng tự chạy và tiếp tục vận hành |
| T07A | Sai mật khẩu/hết quyền database | Không lộ secret, thông báo rõ và không retry dồn dập |
| T07B | Maintenance auto-expiry | Kết thúc đúng hạn, hiển thị/audit đúng và không xóa alarm |
| T07C | Alarm reminder | Nhắc lại không tạo AlarmEvent trùng |

#### Ngày 18 Kiểm thử dữ liệu và hiệu năng

| Mã | Công việc | Điều kiện hoàn thành |
|---|---|---|
| T08 | Test polling 21 tank | Chu kỳ cập nhật đáp ứng cấu hình đã chốt |
| T09 | Test 40 tank/two buses | Không ảnh hưởng lẫn nhau giữa hai tuyến |
| T10 | Query một tháng dữ liệu | Mục tiêu dưới 3 giây trên máy vận hành |
| T11 | Export dữ liệu lớn | Không treo UI, file hợp lệ |
| T12 | Restart recovery | Không tạo alarm hoặc sample trùng bất hợp lý |
| T13 | So sánh PV/SV | Khớp màn hình controller |
| T14 | Dữ liệu mô phỏng 12 tháng/40 tank | Insert, retention, partition và query đạt mục tiêu; có báo cáo dung lượng/query plan |

**Cổng kiểm thử:** không còn lỗi Critical/Blocker; tất cả test P0 trong phạm vi tuần 1–4 phải đạt.

#### Ngày 19 Đóng gói và tài liệu

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| DEP01 | Build Release x64 | Release package | Build sạch và chạy trên máy không có Visual Studio |
| DEP02 | Installer | Bộ cài đặt | Cài mới, nâng cấp và gỡ cài đặt được |
| DEP03 | Tài liệu cài đặt | Installation guide | IT có thể thực hiện theo tài liệu |
| DEP04 | Tài liệu vận hành | User guide | Operator thực hiện được thao tác chính |
| DEP05 | Tài liệu xử lý lỗi | Troubleshooting guide | Có hướng dẫn COM, offline, backup và log |
| DEP06 | Release notes | Danh sách thay đổi | Có version, ngày phát hành và known issues |
| DEP07 | Dependency/license review | SBOM/dependency register | Có tên/version/source/license/trạng thái duyệt; không còn Critical/High chưa xử lý |
| DEP08 | Git hygiene | `.gitignore` và Git index sạch | Không track Office/PDF riêng tư, secret, `.vs`, `bin`, `obj`, executable/log/runtime data |
| DEP09 | Watchdog/Task Scheduler | Startup/recovery configuration | Heartbeat, restart limit, backoff và log nguyên nhân hoạt động |

#### Ngày 20 UAT nội bộ và Feature Complete

| Mã | Công việc | Điều kiện hoàn thành |
|---|---|---|
| UAT01 | Demo quy trình vận hành | Người dùng thực hiện được dashboard, acknowledge và tra cứu |
| UAT02 | Chạy bộ test nghiệm thu | Tất cả P0 đạt, không còn blocker |
| UAT03 | Ghi nhận phản hồi | Mọi thay đổi được phân loại bug hoặc change request |
| UAT04 | Khóa phạm vi release | Không thêm chức năng mới vào RC |
| UAT05 | Khóa Feature Complete | Không thêm tính năng mới; danh sách bug/hardening tuần 5 rõ ràng |

**Cổng M4:** chức năng P0/P1 đã hoàn thành; backup/restore, watchdog, user identity, dependency review và fault injection đạt. Tuần 5 chỉ hardening, regression, tài liệu và sửa bug.

### Tuần 5 Hardening và Release Candidate

#### Ngày 21 PostgreSQL durability và capacity

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| HD01 | Nạp dữ liệu 12 tháng/40 tank | Dataset/report | Dung lượng, insert rate và query plan được ghi nhận |
| HD02 | Query/partition/retention | Performance report | Query một tháng/tank dưới 3 giây trên máy chuẩn; dọn partition đúng chính sách |
| HD03 | Queue 24 giờ và replay | Recovery report | Restart/mất mạng không mất dữ liệu trong dung lượng thiết kế, không ghi trùng |
| HD04 | Backup/restore rehearsal | Database kiểm thử | `pg_dump`/`pg_restore` hoàn chỉnh và ứng dụng chạy trên bản restore |

#### Ngày 22 Alarm, maintenance và identity regression

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| HD05 | Alarm source/mismatch | Regression report | Software/Controller/Hardware/Mismatch độc lập và đúng mapping |
| HD06 | Reminder/acknowledge | Regression report | Không trùng event; lưu đúng Operator và thời gian |
| HD07 | Maintenance Mode | Regression report | Reason/user/start/end/expiry/audit đầy đủ, UI luôn dễ nhận biết |
| HD08 | Restart recovery | Regression report | Không nhân đôi active alarm hoặc mất trạng thái quan trọng |

#### Ngày 23 Installer, watchdog và nâng cấp

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| HD09 | Cài mới/nâng cấp/gỡ cài đặt | Installer report | Không cần Visual Studio; không mất config/database |
| HD10 | Task Scheduler/watchdog | Recovery report | Startup, heartbeat, restart limit/backoff và log đúng |
| HD11 | Máy không đăng nhập/reboot | Quyết định vận hành | Hành vi đúng phạm vi; nếu cần acquisition không login thì mở change request Windows Service |
| HD12 | Security/dependency scan | Báo cáo/SBOM | Không còn secret, dependency Critical/High hoặc license chưa xử lý |

#### Ngày 24 Full regression và bắt đầu soak test

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| RC01 | Chạy toàn bộ test P0/P1 | Test report | Tất cả test bắt buộc đạt, không còn Critical/High |
| RC02 | Kiểm tra traceability | Matrix cập nhật | Mọi FR/NFR P0/P1 có task, test và link bằng chứng |
| RC03 | Khởi động soak test | Log/monitoring | 21 tank + PostgreSQL + alarm chạy liên tục, theo dõi memory/queue/timeout |
| RC04 | UAT nội bộ | Biên bản nội bộ | Operator thực hiện dashboard, ack, maintenance, query và CSV |

#### Ngày 25 Kết thúc soak nội bộ và phát hành RC

| Mã | Công việc | Đầu ra | Điều kiện hoàn thành |
|---|---|---|---|
| RC05 | Đánh giá soak nội bộ | Stability report | Không crash/treo/mất dữ liệu nghiêm trọng; mọi bất thường có kết luận |
| RC06 | Khóa release | Release package/checksum | Version, source, installer, migration, config và checksum đầy đủ |
| RC07 | Hoàn thiện tài liệu | Bộ tài liệu | Installation/User/Admin/Troubleshooting/Backup/Release notes đồng bộ |
| RC08 | Go/No-Go | Biên bản quyết định | Chỉ Go khi traceability/DoD đạt và không còn lỗi Critical/High |

**Cổng M5:** Release Candidate chỉ được đưa ra hiện trường khi có Go, backup restore được, traceability đầy đủ và không còn lỗi Critical/High.

## 8. Tuần SAT/UAT hiện trường

### Ngày 26–27 Cài đặt và SAT

- Cài đặt trên máy giám sát thực tế.
- Cấu hình COM port/gateway thực tế.
- Import danh sách tank.
- Kiểm tra lần lượt từng địa chỉ Modbus.
- So sánh PV/SV giữa phần mềm và controller.
- Mô phỏng ít nhất một High, Low, Sensor Error và Offline.
- Kiểm tra đường dẫn backup và export.
- Kiểm tra PostgreSQL service, tài khoản ứng dụng, firewall/TLS và phiên bản migration.
- Dừng PostgreSQL có kiểm soát để xác nhận cảnh báo, hàng đợi cục bộ và ghi bù sau phục hồi.

**Tiêu chí hoàn thành:** tất cả 21 tank online hoặc có biên bản ghi rõ điểm chưa sẵn sàng do phần cứng; dữ liệu hợp lệ không bị gán nhầm tank.

### Ngày 28–30 Chạy ổn định và UAT

- Chạy liên tục tối thiểu 72 giờ.
- Theo dõi timeout, reconnect, dung lượng database và log.
- Kiểm tra alarm thực tế và lịch sử.
- Sửa lỗi production nếu phát sinh.
- Đào tạo operator và administrator.
- Ký biên bản UAT và bàn giao.

**Cổng M6:** đáp ứng toàn bộ Definition of Done trong tài liệu yêu cầu, ma trận truy vết đã cập nhật bằng chứng SAT/UAT và biên bản được ký.

## 9. Tiêu chí nghiệm thu định lượng

| Nhóm | Tiêu chí |
|---|---|
| Build | Build Release thành công, không có lỗi biên dịch |
| Driver | CRC, frame parser và exception handling vượt toàn bộ test |
| Độ chính xác | PV/SV khớp giá trị hiển thị trên controller theo độ phân giải thiết bị |
| Register/scale | Giá trị âm, decimal, sensor error và bit alarm khớp register map/controller thật |
| Polling | 21 tank được cập nhật trong chu kỳ đã thống nhất |
| Offline backoff | Nhiều thiết bị Offline không làm thiết bị Online vượt chu kỳ đã chốt; không retry vô hạn |
| Mở rộng | Simulator hoặc hệ thống test chạy được 40 tank trên ít nhất hai connection |
| UI | Không treo khi nhiều thiết bị timeout; hiển thị rõ ở 1920 x 1080 |
| Reconnect | Tự kết nối lại sau khi đường truyền được phục hồi, mục tiêu không quá 30 giây |
| Alarm | Không tạo alarm trùng; lưu đúng source/start/end/acknowledge và phát hiện mismatch |
| Maintenance | Có reason/user/start/end/expiry/audit; hiển thị rõ và không xóa alarm |
| Dữ liệu | Không lưu giá trị lỗi như PV hợp lệ; timestamp và tank mapping chính xác |
| Query | Truy vấn một tháng của một tank mục tiêu dưới 3 giây |
| Export | CSV mở được, không cần Excel và khớp dữ liệu được lọc; Excel chỉ nghiệm thu khi nằm trong phạm vi đã duyệt |
| Backup | Backup được restore thành công trên môi trường kiểm thử |
| PostgreSQL | Ứng dụng dùng role quyền tối thiểu; migration đúng phiên bản; mất kết nối không dừng polling và ghi bù không trùng |
| Ổn định | Chạy liên tục tối thiểu 72 giờ không crash hoặc mất dữ liệu nghiêm trọng |
| Bảo mật | Operator không thay đổi được cấu hình quản trị |
| Audit identity | Acknowledge/config lưu đúng người; không dùng tài khoản chung không truy vết được |
| Watchdog | Heartbeat, startup, restart limit/backoff và log nguyên nhân hoạt động đúng |
| Traceability | Mọi FR/NFR P0/P1 có task, test và bằng chứng kết quả |
| Dependency | Có SBOM/dependency register; không còn license hoặc lỗ hổng Critical/High chưa xử lý |
| Triển khai | Cài đặt được trên máy không có Visual Studio |
| UAT | Không còn lỗi Critical/High và có xác nhận của đại diện vận hành |

## 10. Kiểm soát Definition of Done

Definition of Done duy nhất của project nằm tại phần 13 của [tài liệu yêu cầu](./Yeu-cau-project-phan-mem-giam-sat-hoa-chat-SEMV.md). Lộ trình này không duy trì một bản DoD thứ hai để tránh sai khác.

Trước khi qua cổng M5/M6, người phụ trách phải:

- [ ] Đánh dấu kết quả từng yêu cầu P0/P1 trong [Traceability Matrix](./Traceability-Matrix-SEMV.md).
- [ ] Gắn đường dẫn hoặc mã báo cáo test, ảnh, log, biên bản cho từng test bắt buộc.
- [ ] Xác nhận không còn dòng P0/P1 ở trạng thái Missing/Blocked/Failed.
- [ ] Xác nhận mọi ngoại lệ có owner, mức rủi ro và phê duyệt bằng văn bản.
- [ ] Xác nhận source, migration, installer, SBOM/dependency register, cấu hình mẫu và tài liệu bàn giao cùng version Release Candidate.

## 11. Danh sách sản phẩm bàn giao

| Sản phẩm | Nội dung tối thiểu |
|---|---|
| Source code | Toàn bộ C# source, solution và project files |
| Release package | Executable, dependencies và configuration mặc định |
| Installer | Cài mới, nâng cấp và gỡ cài đặt |
| Database | PostgreSQL schema, migration, partition/index, script role/quyền và dữ liệu mẫu không nhạy cảm |
| Device configuration | Danh sách tank, connection và Modbus address |
| Test tools | Simulator hoặc chương trình kiểm tra Modbus |
| Test report | Unit, integration, SAT, soak test và UAT |
| Traceability | Ma trận FR/NFR–task–test–bằng chứng đã cập nhật trạng thái cuối |
| Dependency/SBOM | Tên, version, nguồn, license, mục đích, trạng thái phê duyệt và kết quả kiểm tra bảo mật |
| Installation guide | Yêu cầu máy, cài đặt, cấu hình và nâng cấp |
| User guide | Dashboard, alarm, lịch sử, export và acknowledge |
| Admin guide | Device, threshold, account, backup và restore |
| Troubleshooting | COM port, timeout, offline, PostgreSQL, queue, watchdog, log và phục hồi |
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
| Sai signed/scale/decimal | Hiển thị và cảnh báo sai nhiệt độ | Test giá trị âm/biên, register catalog có nguồn và đối chiếu controller thật |
| Block read không được controller hỗ trợ | Timeout/exception và mất chu kỳ quét | Spike sớm, chỉ dùng khi có bằng chứng và luôn có fallback single read |
| Nhiều thiết bị Offline | Làm chậm toàn tuyến | Retry giới hạn, offline backoff/probe và test tải 21/40 thiết bị |
| Không có thiết bị thật | Không nghiệm thu được driver | Phát triển bằng simulator nhưng dừng cổng nghiệm thu M1/M6 |
| Nhiễu RS485 | Timeout và dữ liệu không ổn định | Logging, retry, kiểm tra topology/cáp/termination với kỹ thuật điện |
| Trùng Modbus address | Dữ liệu sai hoặc xung đột | Validate cấu hình và kiểm tra từng controller |
| Thay đổi 21 thành 40 tank muộn | Trễ UI và truyền thông | Hỗ trợ multi-connection và dynamic dashboard từ đầu |
| Thay đổi ngưỡng alarm | Phải sửa và test lại nghiệp vụ | Lưu threshold trong cấu hình/database, có audit |
| Yêu cầu ghi SV phát sinh | Tăng rủi ro vận hành | Tách change request, thêm phân quyền và xác nhận hai bước |
| Yêu cầu remote access | Tăng phạm vi và bảo mật | Tách giai đoạn 2, cần IT phê duyệt |
| Antivirus chặn ứng dụng | Không triển khai được | Kiểm thử installer sớm trên môi trường IT cung cấp |
| Database tăng nhanh | Chậm truy vấn/đầy ổ | Partition theo thời gian, retention, index, capacity test, backup và cảnh báo dung lượng |
| PostgreSQL service/mạng bị gián đoạn | Mất khả năng ghi lịch sử | Health check, reconnect có backoff, hàng đợi bền vững 24 giờ và ghi bù idempotent |
| Sai cấu hình quyền/TLS/firewall | Không kết nối được hoặc tăng rủi ro bảo mật | Kiểm tra sớm với IT, dùng role quyền tối thiểu và không public cổng database |
| Nguồn alarm không thống nhất | Operator hiểu sai tình trạng | Lưu riêng nguồn, xác nhận AL1/AL2 và AlarmMismatch có delay |
| Tài khoản Operator dùng chung | Audit không có giá trị | Quick switch/Windows identity; acknowledge gắn đúng phiên người dùng |
| Watchdog restart lặp | Gián đoạn vận hành | Restart limit, backoff, heartbeat và yêu cầu can thiệp sau ngưỡng |
| License/dependency chưa duyệt | Không được phép phát hành | Review ở M0, CSV bắt buộc, khóa version và SBOM trước RC |

## 14. Quy tắc xử lý blocker và thay đổi

- Blocker phải ghi rõ nguyên nhân, người phụ trách và ngày cần phản hồi.
- M0 và thời gian bị chặn do thiếu controller, register map, môi trường PostgreSQL, dependency/license hoặc quyền IT không tính vào 25 ngày phát triển.
- Bug làm sai dữ liệu, mất alarm hoặc crash được xếp mức Critical/High và phải sửa trước release.
- Yêu cầu mới ngoài phạm vi phiên bản 1 phải tạo change request.
- Remote access, ghi SV, tích hợp MES/SCADA và nhiều máy trạm là thay đổi phạm vi đáng kể.
- Sau ngày 20 chỉ hardening, hoàn thiện test/tài liệu và sửa bug cho Release Candidate; không thêm tính năng mới nếu chưa đánh giá lại tiến độ.
- Windows Service, remote access, ghi SV, notification, MES/SCADA và Excel khi chưa duyệt license phải được quản lý như change request/P2.

## 15. Theo dõi tiến độ hàng ngày

Mỗi ngày cần cập nhật tối thiểu:

- Task đã hoàn thành.
- Task đang thực hiện.
- Test đã chạy và kết quả.
- Bug mới phát hiện.
- Blocker và người cần xử lý.
- Thay đổi phạm vi nếu có.
- Dòng traceability được cập nhật và link bằng chứng test.
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

Lộ trình mục tiêu gồm M0 ngoài thời gian, 25 ngày phát triển và 5 ngày SAT/UAT hiện trường. Driver Modbus, bằng chứng register/scale, mô hình nguồn alarm và môi trường PostgreSQL là các đầu vào đường găng. Phần mềm không được nghiệm thu nếu chưa kiểm thử trên controller thật, chưa restore backup PostgreSQL thành công hoặc ma trận truy vết còn yêu cầu P0/P1 thiếu bằng chứng.

Để giữ tiến độ 6 tuần, toàn bộ checklist M0 phải hoàn thành trước ngày phát triển đầu tiên. Tuần cuối dành cho SAT, chạy ổn định tối thiểu 72 giờ, đào tạo và UAT; không dùng tuần này để hoàn thiện tính năng chưa xong.
