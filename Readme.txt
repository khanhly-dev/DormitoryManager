- nodejs 16 (required)
- net core 5 (required)
- angular (required)
- visual studio 

cách chạy chương trình
1: vào thư mục DormitoryManager chạy file DormitoryManager.sln bằng visual studio
2: bấm chuột phải vào project "Dormitory.EntityFrameworkCore" chọn "set as Startup project"
3: trên thanh menu chọn tool -> NuGet package manager -> package manager console
4: phía dưới màn hình hiển thị 1 cửa sổ console, ở mục default project chọn "Dormitory.EntityFrameworkCore"
5: tại màn hình còn console chạy lệnh "update-database"
6: chạy 3 project 
 - Dormitory.Admin.Api
 - Dormitory.Core.Api
 - Dormitory.Student.Api
 tương ứng với 3 cổng api của backend
7: mở cmd tại đường dẫn của 2 thư mục Dormitory.Admin.Web và Dormitory.Student.Web và chạy lệnh "npm install" để cài đặt node_module
8: sau đó chạy 2 project angular này bằng cách chạy câu lệnh "ng serve"
9: copy cổng mà angular lắng nghe trên cmd paste lên trình duyệt web
10: chương trình đã khởi chạy thành công

