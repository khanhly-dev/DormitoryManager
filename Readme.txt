- nodejs 16 (required)
- net core 5 (required)
- angular (required)
- visual studio 

cách chạy chương trình
1: vào thư mục DormitoryManager chạy file DormitoryManager.sln bằng visual studio
2: chạy 3 project 
 - Dormitory.Admin.Api
 - Dormitory.Core.Api
 - Dormitory.Student.Api
 tương ứng với 3 cổng api của backend
3: mở cmd tại đường dẫn của 2 thư mục Dormitory.Admin.Web và Dormitory.Student.Web và chạy lệnh "npm install" để cài đặt node_module
4: sau đó chạy 2 project angular này bằng cách chạy câu lệnh "ng serve"
5: copy cổng mà angular lắng nghe trên cmd paste lên trình duyệt web
6: chương trình đã khởi chạy thành công

* chú ý: có thể chạy 3 project Dormitory.Admin.Api, Dormitory.Core.Api và Dormitory.Student.Api mà không
cần sử dụng visual studio bằng cách mở cmd tại đường dẫn của từng thư mục và chạy lệnh "dot net run"