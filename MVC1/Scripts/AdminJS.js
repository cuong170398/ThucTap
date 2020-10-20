function functionUpdate() {
        document.getElementById("AddAdmin").style.display = "block";
    }
    function funcBack() {
        document.getElementById("AddAdmin").style.display = "none";
    }
    function Add() {
        var empObj = {
        //Admin_id: $('#Admin_id').val(),
        Permission_id: $('#Permission_id').val(),
    Frist_name: $('#Frist_name').val(),
    Last_name: $('#Last_name').val(),
    Email: $('#Email').val(),
    PassW: $('#PassW').val(),
    Addres: $('#Addres').val(),
    birthday: $('#birthday').val(),
    Gender: $('#Gender').val(),
    Salary: $('#Salary').val(),
    Admin_Status: $('#Admin_Status').val()
};
        $.ajax({
        url: "/Admin/Admin/Add",
    contentType: "application/json;charset=UTF-8",
    type: "GET",
    data: empObj,
    dataType: "json",
            success: function (result) {
        alert("Thêm mới thành công !");
    location.reload("AddAdmin");
},
            error: function (errormessage) {
        alert(errormessage.responseText);
    }
});
//document.getElementById("updateAdmin").style.display = "none";
}
    function ShowEdit(ID) {
        document.getElementById('AddAdmin').style.display = "block";
    $('#Editnew').show();
    $('#Addnew').hide();
    $('#titleEdit').show();
    $('#titleAddnew').hide();
        $.ajax({
        url: "/Admin/Admin/ShowEdit/" + ID,
    type: "GET",
    contentType: "application/json;charset=UTF-8",
    dataType: "json",
            success: function (result) {
                var dt = new Date(parseInt(result.birthday.replace('/Date(', '')));
    var dtFinal = AddLeadingZezos(dt.getMonth() + 1, 2) + '/' +
        AddLeadingZezos(dt.getDate(), 2) + '/' +
        AddLeadingZezos(dt.getFullYear(), 4);
    $('#Admin_id').val(result.Admin_id),
        $('#Permission_id').val(result.Permission_id),
        $('#Frist_name').val(result.Frist_name),
        $('#Last_name').val(result.Last_name),
        $('#Email').val(result.Email),
        $('#PassW').val(result.PassW),
        $('#Addres').val(result.Addres),
        $('#birthday').val(dtFinal),
        $('#Gender').val(result.Gender),
        $('#Salary').val(result.Salary),
        result.Admin_Status === true ? document.getElementById('opTrue').selected = "selected" : document.getElementById('opFalse').selected = "selected",
        console.log(result.Admin_Status)
            }, error: function (errormessage) {
        alert(errormessage.responseText);
    }
});
}
    function AddLeadingZezos(number, Size) {
        var s = "0000" + number;
    return s.substr(s.length - Size);
}
    function Edit() {
        $('#Editnew').show();
    $('#Addnew').hide();
        var empObj = {
        Admin_id: $('#Admin_id').val(),
    Permission_id: $('#Permission_id').val(),
    Frist_name: $('#Frist_name').val(),
    Last_name: $('#Last_name').val(),
    Email: $('#Email').val(),
    PassW: $('#PassW').val(),
    Addres: $('#Addres').val(),
    birthday: $('#birthday').val(),
    Gender: $('#Gender').val(),
    Salary: $('#Salary').val(),
    Admin_Status: $('#Admin_Status').val()
};
        $.ajax({
        url: "/Admin/Admin/Edit/",
    contentType: "application/json;charset=UTF-8;",
    type: "GET",
    data: empObj,
    dataType: "json",
            success: function (result) {

        alert("Sửa bản ghi thành công !");
    location.reload("AddAdmin");
},
            error: function (errormessage) {
        alert(errormessage.responseText);
    }
});
}
    function Delele(ID) {
        var ans = confirm("Bạn có chắc chắn muốn xóa dòng này không ?");
        if (ans) {
        $.ajax({
            url: "/Admin/Admin/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                alert("Xóa bản ghi thành công !");
                location.reload("tblCustomers");
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}