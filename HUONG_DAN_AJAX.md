# 📚 HƯỚNG DẪN: LỌC SẢN PHẨM BẰNG AJAX

## 🎯 Mục tiêu
Tạo một menu cho phép click vào để lọc sản phẩm theo danh mục (VD: chất liệu) **mà không reload trang** bằng AJAX.

---

## 📋 4 BƯỚC CHÍNH

### **BƯỚC 1: Sửa Menu ViewComponent**
📁 File: `Views/Shared/Components/ChatLieuMenu/Default.cshtml`

**Hiện tại:**
```html
<li><a href="#">@item.ChatLieu</a></li>
```

**Sửa thành:**
```html
<li><a href="#" class="filter-chattieu" data-id="@item.MaChatLieu">@item.ChatLieu</a></li>
```

**Giải thích:**
- `class="filter-chattieu"` → Dùng để jQuery tìm và lắng nghe click
- `data-id="@item.MaChatLieu"` → Lưu ID của chất liệu để gửi lên server

---

### **BƯỚC 2: Thêm Action mới trong Controller**
📁 File: `Controllers/HomeController.cs`

**Thêm method mới:**
```csharp
public IActionResult GetProductByChatLieu(string maChatLieu)
{
    var products = db.TDanhMucSps
        .Where(x => x.MaChatLieu == maChatLieu)
        .ToList();
    
    return PartialView("_ProductList", products);
}
```

**Giải thích:**
- Nhận tham số `maChatLieu` từ AJAX gửi lên
- Lọc sản phẩm từ database
- Trả về **PartialView** (chỉ HTML, không layout)

---

### **BƯỚC 3: Tạo Partial View hiển thị sản phẩm**
📁 File: `Views/Home/_ProductList.cshtml` (Tạo file mới)

```html
@model List<TH1.Models.TDanhMucSp>

@foreach (var item in Model)
{
    <div class="col-md-6 col-lg-4 mb-5">
        <div class="hotel-room text-center">
            <a href="#" class="d-block mb-0 thumbnail">
                <img src="../LayoutHotel/images/img_1.jpg" class="img-fluid">
            </a>

            <div class="hotel-room-body">
                <h3 class="heading mb-0">
                    <a href="#">@item.TenSp</a>
                </h3>
            </div>
        </div>
    </div>
}

@if (Model.Count == 0)
{
    <div class="col-12 text-center mt-5">
        <p class="text-muted">Không có sản phẩm nào</p>
    </div>
}
```

**Giải thích:** Đây là HTML của danh sách sản phẩm, server sẽ trả về để AJAX update vào trang

---

### **BƯỚC 4: Thêm ID + JavaScript vào View chính**
📁 File: `Views/Home/Index.cshtml`

**A) Sửa div chứa sản phẩm:**
```html
<div class="row" id="productList">
    @foreach (var item in Model)
    {
        <!-- HTML sản phẩm -->
    }
</div>
```

**Chú ý:** Thêm `id="productList"` để JavaScript biết update vào đâu

**B) Thêm JavaScript vào _Layout.cshtml** (không phải Index)

📁 File: `Views/Shared/_Layout.cshtml`

**Tìm chỗ có:**
```html
<script src="../LayoutHotel/js/main.js"></script>
```

**Thêm script AJAX SAU nó:**
```javascript
<script>
    $(document).ready(function () {
        // Lắng nghe click trên link chất liệu
        $(document).on('click', '.filter-chattieu', function (e) {
            e.preventDefault();
            
            // Lấy ID chất liệu từ data attribute
            var maChatLieu = $(this).data('id');
            
            // Gửi AJAX request tới server
            $.ajax({
                type: 'GET',
                url: '/Home/GetProductByChatLieu',
                data: { maChatLieu: maChatLieu },
                success: function (data) {
                    // Update danh sách sản phẩm
                    $('#productList').html(data);
                },
                error: function () {
                    alert('Lỗi!');
                }
            });
        });
    });
</script>
```

**⚠️ QUAN TRỌNG:** Script phải ở **SAU khi jQuery được load**, nên thêm vào _Layout.cshtml chứ không phải Index.cshtml

---

## 🔄 QUY TRÌNH HOẠT ĐỘNG

```
Trang load
    ↓
Hiển thị sản phẩm từ Model (loop @foreach)
    ↓
User click vào "Chất liệu" trong menu
    ↓
JavaScript jquery bắt sự kiện click (class="filter-chattieu")
    ↓
Gửi AJAX request: /Home/GetProductByChatLieu?maChatLieu=ABC
    ↓
Server xử lý: GetProductByChatLieu("ABC")
    ↓
Server trả về HTML danh sách sản phẩm
    ↓
JavaScript update HTML vào #productList
    ↓
Danh sách sản phẩm thay đổi ✅ (mà không reload trang)
```

---

## 🐛 TROUBLESHOOTING

### Lỗi: `$ is not defined`
**Nguyên nhân:** jQuery chưa load
**Cách fix:** Đảm bảo script AJAX ở SAU `<script src="jquery-3.3.1.min.js"></script>`

### Lỗi: `404 Not Found`
**Nguyên nhân:** Action không tồn tại hoặc URL sai
**Cách fix:** 
- Kiểm tra method `GetProductByChatLieu` có trong HomeController không?
- URL phải là `/Home/GetProductByChatLieu`

### Sản phẩm không thay đổi
**Nguyên nhân:** File _ProductList.cshtml không tồn tại
**Cách fix:** Kiểm tra có file `Views/Home/_ProductList.cshtml` không?

### DataAttribute bị mất (không có data-id)
**Nguyên nhân:** Quên sửa menu ViewComponent
**Cách fix:** Kiểm tra `ChatLieuMenu/Default.cshtml` có `data-id="@item.MaChatLieu"` không?

---

## 💡 MẘI ĐIỂM CẦN NHỚ

1. **Menu:** Thêm `class="filter-chattieu"` + `data-id`
2. **Controller:** Tạo action mới trả về PartialView
3. **PartialView:** Là HTML danh sách sản phẩm
4. **Index View:** Thêm `id="productList"` vào div chứa sản phẩm
5. **_Layout.cshtml:** Thêm script AJAX **SAU jQuery load**

---

## 📌 VÍ DỤ THỰC TẾ

Nếu muốn lọc sản phẩm theo **Hãng sản xuất** thay vì **Chất liệu**:

1. **Menu:** Tạo ViewComponent `HangSxMenu` tương tự
2. **Controller:** Thêm method `GetProductByHangSx(string maHangSx)`
3. **PartialView:** Tạo `_ProductList.cshtml` (dùng chung)
4. **JavaScript:** Sửa selector từ `.filter-chattieu` → `.filter-hangsx`

**Quy trình vẫn giống nhất, chỉ thay đổi tên biến/class thôi!**

