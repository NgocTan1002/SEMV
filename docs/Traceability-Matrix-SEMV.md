# Ma trận truy vết yêu cầu – công việc – kiểm thử SEMV

## 1. Mục đích

Tài liệu này liên kết yêu cầu trong [Yêu cầu project phần mềm giám sát nhiệt độ hóa chất SEMV](./Yeu-cau-project-phan-mem-giam-sat-hoa-chat-SEMV.md) với công việc trong [Lộ trình hoàn thiện project](./Lo-trinh-hoan-thien-project-phan-mem-SEMV.md), test case và bằng chứng nghiệm thu.

Tài liệu yêu cầu là nguồn chuẩn của FR/NFR và Definition of Done. Ma trận này chỉ theo dõi việc thực hiện, không được dùng để thay đổi nội dung yêu cầu.

## 2. Quy ước

| Trường | Ý nghĩa |
|---|---|
| Priority | P0: bắt buộc/đường găng; P1: bắt buộc cho production; P2: giai đoạn sau hoặc change request |
| Status | Not Started, Ready, In Progress, Blocked, Passed, Failed hoặc Accepted |
| Evidence | Đường dẫn/mã unit test, test report, log, ảnh, biên bản hoặc commit/tag; không ghi “đã test” nếu thiếu bằng chứng |

Quy tắc cập nhật:

- Mỗi pull request hoặc lần bàn giao chức năng phải cập nhật các dòng liên quan.
- Một dòng chỉ chuyển sang `Passed` khi test đạt và có Evidence.
- Chỉ chuyển sang `Accepted` sau khi người có thẩm quyền ký SAT/UAT hoặc chấp thuận kỹ thuật tương ứng.
- Release Candidate không được phát hành nếu còn dòng P0/P1 ở trạng thái `Missing`, `Blocked` hoặc `Failed` mà chưa có phê duyệt ngoại lệ bằng văn bản.

## 3. Ma trận yêu cầu chức năng

| Requirement | Priority | Nội dung kiểm soát | Roadmap task | Test/Acceptance | Status | Evidence/Owner |
|---|---|---|---|---|---|---|
| FR-01 | P1 | Quản lý tank, mã duy nhất, mapping đúng device | CFG01, CFG02 | AT-01, AT-10 | Not Started | Dev/QA |
| FR-02 | P0 | Multi-bus, cấu hình serial, reconnect và cô lập tuyến | C13, C14, P01, P04 | AT-02, AT-03, AT-13, AT-14 | Not Started | Dev/Automation |
| FR-03A | P0 | Parser signed/unsigned, scale, decimal và sensor error | A02–A04, C15–C17 | AT-01, AT-07, AT-16 | Blocked | Cần register map/controller thật; Automation |
| FR-03B | P0 | Block read spike và fallback single-register | C10–C12 | AT-17 | Blocked | Cần controller/manual; Dev/Automation |
| FR-03C | P0 | Offline backoff, retry giới hạn, không chặn bus | C13, C14, U14, U15 | AT-13, AT-14, AT-18 | Not Started | Dev/QA |
| FR-04 | P1 | Dashboard 21 tank, mở rộng 40, trạng thái rõ | U01–U10 | AT-01, AT-04, AT-05, AT-07 | Not Started | Dev/Operations |
| FR-05A | P0 | High/Low/Sensor/Communication state machine | AL01–AL05 | AT-02–AT-07 | Not Started | Dev/QA/EHS |
| FR-05B | P0 | Tách Software/Controller/Hardware Alarm | AL05A, C16, HD05 | AT-19 | Blocked | Cần cấu hình AL1/AL2; Automation/EHS |
| FR-05C | P0 | AlarmMismatch không ghi đè nguồn alarm | AL05B, HD05 | AT-19 | Not Started | Dev/QA |
| FR-05D | P1 | Alarm reminder không tạo event trùng | AL11, HD06 | AT-20 | Not Started | Dev/Operations |
| FR-06 | P1 | Lịch sử, start/end/ack/note và bộ lọc | AL06–AL10, H06 | AT-08, AT-10, AT-25 | Not Started | Dev/QA |
| FR-07A | P0 | Historian, UTC, quality, retention và partition | H01–H04, HD01, HD02 | AT-10, AT-23 | Not Started | Dev/DBA |
| FR-07B | P0 | Durable queue 24 giờ và replay idempotent | D08, P06, HD03 | AT-22 | Not Started | Dev/QA |
| FR-08 | P1 | Trend/history, gap dữ liệu và đường ngưỡng | H05–H07 | AT-10 | Not Started | Dev/Operations |
| FR-09A | P1 | CSV bắt buộc, không phụ thuộc Excel | H08 | AT-11 | Not Started | Dev/QA |
| FR-09B | P2 | Excel khi license được duyệt | H09 | AT-11 | Blocked | IT/Legal/Product Owner |
| FR-10 | P1 | Operator/Admin, quick switch, session timeout | SEC01, SEC02, SEC04, SEC05 | AT-08, AT-25 | Not Started | Dev/IT/Operations |
| FR-11 | P1 | Audit đăng nhập, cấu hình, threshold và acknowledge | SEC03, AL12 | AT-08, AT-21, AT-25 | Not Started | Dev/QA |
| FR-12 | P0 | PostgreSQL backup/restore đã diễn tập | BK01–BK03, HD04 | AT-12, AT-22 | Not Started | Dev/IT/DBA |
| FR-13 | P1 | Log/chẩn đoán không lộ secret, raw frame có kiểm soát | C05, D07, DEP05 | Fault injection report | Not Started | Dev/IT |
| FR-14 | P1 | Maintenance reason/user/start/end/expiry/audit | CFG06, CFG07, T07B, HD07 | AT-21 | Not Started | Dev/Operations/QA |
| FR-15 | P1 | Health, heartbeat, queue, backup và watchdog | DEP09, HD10, HD11 | AT-24 | Not Started | Dev/IT/QA |

## 4. Ma trận yêu cầu phi chức năng

| Requirement | Priority | Nội dung kiểm soát | Roadmap task | Test/Acceptance | Status | Evidence/Owner |
|---|---|---|---|---|---|---|
| NFR-01 | P0 | Polling không khóa UI; query tháng dưới 3 giây | U11–U15, T08–T11, HD01, HD02 | AT-13, AT-18, AT-23 | Not Started | Dev/QA |
| NFR-02 | P0 | RS485/PostgreSQL lỗi không crash; tự phục hồi | C18, P01–P06, T01–T07 | AT-02, AT-03, AT-14, AT-22 | Not Started | Dev/QA |
| NFR-03 | P0 | Transaction, UTC, migration, backup và retention an toàn | D01–D08, H01–H04, BK01–BK03 | AT-10, AT-12, AT-22, AT-23 | Not Started | Dev/DBA |
| NFR-04 | P0 | Least privilege, secret protection, firewall/TLS | D05, D06, T07A, HD12 | Security checklist/report | Blocked | Cần IT phê duyệt; IT/Dev |
| NFR-05 | P1 | Module hóa, register catalog, unit test, simulator | A01–A05, C01–C09 | Unit/fault injection report | Not Started | Dev/QA |
| NFR-06 | P1 | Windows x64, DPI, installer, upgrade và startup | U13, DEP01, DEP02, DEP09, HD09–HD11 | Install/upgrade/watchdog report | Not Started | Dev/IT |
| NFR-07 | P1 | Dependency/license/SBOM và security scan | DEP07, HD12 | SBOM + approval + scan | Blocked | IT/Legal/Product Owner |

## 5. Ma trận công việc quản trị P0/P1

| Control | Priority | Điều kiện hoàn thành | Roadmap task | Status | Evidence/Owner |
|---|---|---|---|---|---|
| M0 đầu vào | P0 | Toàn bộ checklist phần 4 của lộ trình đạt trước ngày 1 | M0 | Blocked | Product Owner |
| Register evidence | P0 | Register catalog có nguồn và test controller thật | A02–A04, C15–C17 | Blocked | Automation |
| Throughput decision | P0 | Có số liệu block/single read và chiến lược fallback | C10–C12 | Blocked | Dev/Automation |
| Alarm source decision | P0 | AL1/AL2 và nguồn hardware/controller được ký xác nhận | C16, AL05A/B | Blocked | Automation/EHS |
| Git hygiene | P0 | `.gitignore` đầy đủ, generated/private files không còn trong commit mới | DEP08 | Passed | `git ls-files` không còn Office/PDF hoặc `.vs/bin/obj`; Dev; 08/10/2026 |
| Watchdog architecture | P0 | Xác nhận WinForms + Task Scheduler hoặc mở change request Windows Service | M0, DEP09, HD11 | Ready | Product Owner/IT |
| Traceability | P0 | Mọi P0/P1 có task/test/evidence và trạng thái | A05, RC02 | In Progress | QA/Dev |
| User identity | P1 | Không dùng account chung; ack gắn đúng user | SEC04, AL12 | Blocked | Operations/IT |
| Maintenance | P1 | Chính sách suppress/expiry/audit được duyệt | CFG06, CFG07 | Blocked | Operations/EHS |
| CSV/Excel scope | P1/P2 | CSV bắt buộc; Excel có phê duyệt license hoặc đưa P2 | H08, H09, DEP07 | Blocked | Product Owner/IT/Legal |

## 6. Cổng phát hành

### Gate M1 — Driver

- FR-02, FR-03A/B/C và các test AT-16/17/18 không còn `Blocked/Failed`.
- Có bằng chứng controller thật hoặc biên bản giới hạn phần chỉ được nghiệm thu bằng simulator.

### Gate M2 — Data và dashboard

- FR-04, FR-07B, NFR-01/02/03 đạt test trong phạm vi tuần 2.
- PostgreSQL migration/role/queue và dashboard 21 tank chạy được.

### Gate M3 — Alarm và historian

- FR-05A/B/C/D, FR-06, FR-07A, FR-08, FR-09A, FR-14 đạt.
- Không có alarm/sample trùng trong test restart/replay.

### Gate M4 — Feature Complete

- Toàn bộ FR/NFR P0/P1 đã code complete; các mục còn lại chỉ là bug, hardening hoặc hồ sơ.
- Backup đã restore thực tế, user identity và watchdog đã test.

### Gate M5 — Release Candidate

- Mọi dòng P0/P1 là `Passed` hoặc `Accepted`.
- Không còn lỗi Critical/High; SBOM/license/security scan và tài liệu đồng bộ version.

### Gate M6 — Production Acceptance

- SAT và soak test 72 giờ đạt.
- UAT, đào tạo và bàn giao được ký; bằng chứng cuối được gắn vào ma trận.
