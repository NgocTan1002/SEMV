# Ma trận truy vết yêu cầu – công việc – kiểm thử SEMV

## 1. Mục đích

Tài liệu này liên kết yêu cầu trong [Yêu cầu project phần mềm giám sát nhiệt độ hóa chất SEMV](./Yeu-cau-project-phan-mem-giam-sat-hoa-chat-SEMV.md) với công việc trong [Lộ trình hoàn thiện project](./Lo-trinh-hoan-thien-project-phan-mem-SEMV.md), test case và bằng chứng nghiệm thu.

Tài liệu yêu cầu là nguồn chuẩn của FR/NFR và Definition of Done. Ma trận này chỉ theo dõi việc thực hiện, không được dùng để thay đổi nội dung yêu cầu.

## 2. Quy ước

| Trường | Ý nghĩa |
|---|---|
| Priority | P0: bắt buộc/đường găng; P1: bắt buộc cho production; P2: giai đoạn sau hoặc change request |
| WorkStatus | Not Started, Ready, In Progress, In Review, Blocked hoặc Done; dùng chung với lộ trình |
| VerificationStatus | Not Run, Passed, Failed, Accepted hoặc Waived |
| Evidence | Đường dẫn/mã unit test, test report, log, ảnh, biên bản hoặc commit/tag; không ghi “đã test” nếu thiếu bằng chứng |

Quy tắc cập nhật:

- Mỗi pull request hoặc lần bàn giao chức năng phải cập nhật các dòng liên quan.
- WorkStatus chỉ chuyển `Done` khi implementation/review hoàn thành; trạng thái này không đồng nghĩa test đã đạt.
- VerificationStatus chỉ chuyển `Passed` khi test đạt và có Evidence; `Accepted` sau SAT/UAT hoặc chấp thuận kỹ thuật.
- `Waived` phải có người phê duyệt, lý do, ngày hết hiệu lực và đánh giá rủi ro bằng văn bản.
- Release Candidate không được phát hành nếu dòng P0/P1 có WorkStatus `Blocked` hoặc VerificationStatus `Not Run/Failed`, trừ khi đã `Waived` hợp lệ.

## 3. Ma trận yêu cầu chức năng

| Requirement | Priority | Nội dung kiểm soát | Roadmap task | Test/Acceptance | WorkStatus | VerificationStatus | Evidence/Owner |
|---|---|---|---|---|---|---|---|
| FR-01 | P1 | Tank/Area, mã duy nhất và mapping đúng device | D01, CFG01, CFG02 | AT-01, AT-10 | Not Started | Not Run | Dev/QA |
| FR-02 | P0 | Multi-bus, cấu hình serial, reconnect và cô lập tuyến | C13, C14, P01, P04 | AT-02, AT-03, AT-13, AT-14, AT-31 | Not Started | Not Run | Dev/Automation |
| FR-03A | P0 | Parser signed/unsigned, scale, decimal và sensor error | A02–A04, C15–C17 | AT-01, AT-07, AT-16 | Blocked | Not Run | Cần register map/controller thật; Automation |
| FR-03B | P0 | Block/single read, frame math và multi-rate fallback | C10–C12, T09A | AT-17, AT-18 | Blocked | Not Run | Cần controller/manual; Dev/Automation |
| FR-03C | P0 | Offline probe một lần, backoff và time budget | C13, C14A, U14, U15 | AT-13, AT-14, AT-18 | Not Started | Not Run | Dev/QA |
| FR-04 | P1 | Dashboard 21/40 tank, Area và trạng thái rõ | U01–U10 | AT-01, AT-04, AT-05, AT-07, AT-34 | Not Started | Not Run | Dev/Operations |
| FR-05A | P0 | High/Low/Sensor/Communication state machine | AL01–AL05 | AT-02–AT-07 | Not Started | Not Run | Dev/QA/EHS |
| FR-05B | P0 | Software/Controller threshold và source; LocalHardware là P2 | AL05A, C16, HD05 | AT-19, AT-32 | Blocked | Not Run | Cần AL1/AL2/setpoint; Automation/EHS |
| FR-05C | P0 | AlarmMismatch chỉ cho nguồn có thể so sánh | AL05B, HD05 | AT-19, AT-32 | Not Started | Not Run | Dev/QA |
| FR-05D | P0 | Connection/Bus Alarm và chống alarm flood | AL05C, T07D | AT-31 | Not Started | Not Run | Dev/QA/Operations |
| FR-05E | P1 | Alarm reminder không tạo event trùng | AL11, HD06 | AT-20 | Not Started | Not Run | Dev/Operations |
| FR-06 | P1 | Lịch sử, source/scope/correlation/start/end/ack/note | AL06–AL10, H06 | AT-08, AT-09, AT-10, AT-25 | Not Started | Not Run | Dev/QA |
| FR-07A | P0 | Historian, UTC, quality, retention và capacity baseline | H01–H04, HD01, HD02 | AT-10, AT-15, AT-23 | Not Started | Not Run | Dev/DBA |
| FR-07B | P1 | Buffer/queue theo RPO/RTO và replay idempotent | D08, P06, HD03 | AT-22 | Blocked | Not Run | Cần quyết định RPO/RTO M0; Product Owner/IT |
| FR-08 | P1 | Trend/history, gap dữ liệu, downsampling và đường ngưỡng | H05–H07 | AT-10, AT-34 | Not Started | Not Run | Dev/Operations |
| FR-09A | P1 | CSV bắt buộc, không phụ thuộc Excel | H08 | AT-11 | Not Started | Not Run | Dev/QA |
| FR-09B | P2 | Excel chỉ nâng P1 bằng quyết định M0 | H09 | AT-11, AT-33 | Blocked | Not Run | IT/Legal/Product Owner |
| FR-10 | P1 | Users/Roles/Sessions, quick switch, PIN và timeout | SEC01–SEC08 | AT-08, AT-25, AT-30 | Not Started | Not Run | Dev/IT/Operations |
| FR-11 | P1 | Audit actor/config/threshold/acknowledge/maintenance | SEC02, SEC03, AL12, CFG06 | AT-08, AT-21, AT-25 | Not Started | Not Run | Dev/QA |
| FR-12 | P0 | PostgreSQL backup/restore đã diễn tập | BK01–BK03, HD04 | AT-12, AT-22 | Not Started | Not Run | Dev/IT/DBA |
| FR-13 | P1 | Log/chẩn đoán không lộ secret, raw frame có kiểm soát | C05, D07, DEP05 | AT-26 | Not Started | Not Run | Dev/IT |
| FR-14 | P1 | Maintenance reason/UserId/start/end/expiry/audit | CFG06, CFG07, T07B, HD07 | AT-21 | Not Started | Not Run | Dev/Operations/QA |
| FR-15 | P1 | Health, heartbeat, buffer/queue, backup và watchdog | DEP09, HD10, HD11 | AT-09, AT-15, AT-24, AT-29 | Not Started | Not Run | Dev/IT/QA |

## 4. Ma trận yêu cầu phi chức năng

| Requirement | Priority | Nội dung kiểm soát | Roadmap task | Test/Acceptance | WorkStatus | VerificationStatus | Evidence/Owner |
|---|---|---|---|---|---|---|---|
| NFR-01 | P0 | Polling budget/p95, UI không khóa, query tháng dưới 3 giây | C10–C14A, U11–U15, T08–T11, HD01, HD02 | AT-13, AT-18, AT-23 | Not Started | Not Run | Dev/QA |
| NFR-02 | P0 | RS485/PostgreSQL lỗi không crash và tự phục hồi | C18, P01–P06, T01–T07 | AT-02, AT-03, AT-09, AT-14, AT-15, AT-22, AT-24 | Not Started | Not Run | Dev/QA |
| NFR-03 | P0 | Transaction, UTC, migration, backup, retention và RPO an toàn | D01–D08, H01–H04, BK01–BK03 | AT-10, AT-12, AT-15, AT-22, AT-23 | Not Started | Not Run | Dev/DBA |
| NFR-04 | P0 | Least privilege, secret, PIN, firewall/TLS | D05, D06, SEC07, SEC09, T07A, HD12 | AT-27, AT-30 | Blocked | Not Run | Cần IT phê duyệt; IT/Dev |
| NFR-05 | P1 | Module hóa, register catalog, unit test và simulator | A01–A05, C01–C09 | AT-16, AT-17, AT-18, AT-28 | Not Started | Not Run | Dev/QA |
| NFR-06 | P1 | Windows được hỗ trợ, DPI, installer, upgrade/startup | U13, DEP01, DEP02, DEP09, HD09–HD11 | AT-09, AT-15, AT-24, AT-29 | Not Started | Not Run | Dev/IT |
| NFR-07 | P1 | Dependency/license/SBOM, chart và security scan | DEP07, HD12 | AT-26, AT-33, AT-34 | Blocked | Not Run | IT/Legal/Product Owner |
| NFR-08 | P1 | UI tiếng Việt qua `.resx`, thuật ngữ thống nhất và chart downsampling | U01–U10, H07, DEP07 | AT-29, AT-34 | Not Started | Not Run | Dev/Operations/QA |

## 5. Ma trận công việc quản trị P0/P1

| Control | Priority | Điều kiện hoàn thành | Roadmap task | WorkStatus | VerificationStatus | Evidence/Owner |
|---|---|---|---|---|---|---|
| M0 đầu vào | P0 | Toàn bộ checklist phần 4 của lộ trình đạt trước ngày 1 | M0 | Blocked | Not Run | Product Owner |
| Register evidence | P0 | Register catalog có nguồn và test controller thật | A02–A04, C15–C17 | Blocked | Not Run | Automation |
| Throughput decision | P0 | Frame math + p50/p95/p99 block/single/multi-rate và offline budget | C10–C14A | Blocked | Not Run | Dev/Automation |
| Alarm source decision | P0 | Threshold source, AL1/AL2, bus alarm và điều kiện mismatch được ký | C16, AL05A–C | Blocked | Not Run | Automation/EHS |
| RPO/RTO decision | P0 | Chốt thời gian gián đoạn, data loss cho phép, queue đầy và giải pháp buffer | M0, D08, P06 | Blocked | Not Run | Product Owner/IT |
| Git hygiene | P0 | `docs-private/`/backup/generated/secret không tracked; public PDF/DOCX không bị chặn | DEP08 | Done | Passed | `git check-ignore`/`git ls-files`; Dev; 08/10/2026 |
| Watchdog architecture | P0 | Khóa WinForms watchdog hoặc Windows Service trước ngày 1 | M0 | Blocked | Not Run | Product Owner/IT |
| Chart/dependency decision | P0 | Chart library/version/license/performance và SBOM plan được duyệt | M0, DEP07 | Blocked | Not Run | IT/Legal/Product Owner |
| Traceability | P0 | Mọi P0/P1 có task/test ID/work/verification/evidence | A05, RC02 | In Progress | Not Run | QA/Dev |
| User identity | P1 | SEC01–SEC04 Done trước AL12/CFG06; không dùng account chung | SEC01–SEC04 | Blocked | Not Run | Operations/IT |
| Maintenance | P1 | Chính sách suppress/expiry/audit được duyệt | CFG06, CFG07 | Blocked | Not Run | Operations/EHS |
| CSV/Excel scope | P1/P2 | CSV bắt buộc; Excel P2 trừ khi nâng P1 tại M0 | H08, H09, DEP07 | Ready | Not Run | Product Owner/IT/Legal |

## 6. Cổng phát hành

### Gate M1 — Driver

- FR-02, FR-03A/B/C có WorkStatus `Done` và AT-16/17/18 có VerificationStatus `Passed`.
- Có bằng chứng controller thật hoặc biên bản giới hạn phần chỉ được nghiệm thu bằng simulator.

### Gate M2 — Data và dashboard

- FR-04, FR-07B, FR-10/11 foundation và NFR-01/02/03 đạt test trong phạm vi tuần 2.
- PostgreSQL migration/role, buffer/queue theo RPO, Users/Roles/Audit và dashboard 21 tank chạy được.

### Gate M3 — Alarm và historian

- FR-05A/B/C/D/E, FR-06, FR-07A, FR-08, FR-09A, FR-14 đạt.
- Không có alarm/sample trùng trong test restart/replay.

### Gate M4 — Feature Complete

- Toàn bộ FR/NFR P0/P1 đã code complete; các mục còn lại chỉ là bug, hardening hoặc hồ sơ.
- Backup đã restore thực tế, user identity và watchdog đã test.

### Gate M5 — Release Candidate

- Mọi dòng P0/P1 có WorkStatus `Done` và VerificationStatus `Passed/Accepted`, hoặc `Waived` hợp lệ.
- Không còn lỗi Critical/High; SBOM/license/security scan và tài liệu đồng bộ version.

### Gate M6 — Production Acceptance

- SAT và soak test 72 giờ đạt.
- UAT, đào tạo và bàn giao được ký; bằng chứng cuối được gắn vào ma trận.
