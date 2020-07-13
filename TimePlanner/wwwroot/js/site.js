/// <reference path="../lib/signalr/dist/browser/signalr.js" />
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.


$(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build();
    
    connection.start();

    connection.on("refreshEntries", function () {
        loadData();
    });

    loadData();

    function loadData() { //вывод записей в реальном времени
        var tr = '';
        $.ajax({
            url: '/Home/GetEntries',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    var isCompleted = v.isCompleted ? 'checked' : '';
                    var place = v.place;
                    var endTime = v.endTime;
                    if (v.place == null) place = '';
                    if (v.endTime == '0001-01-01T00:00:00') endTime = '';
                    tr = tr + `<tr>
                        <td>
                            <label class="switch">
                            <input type="checkbox" name="${v.id}" ${isCompleted}>
                            <span class="slider round" onclick="changeCompleteStatus('${v.id}')"></span>
                            </label>
                        </td>
                        <td>${v.type}</td>
                        <td>${v.theme}</td>
                        <td>${v.startTime}</td>
                        <td>${endTime}</td>
                        <td>${place}</td>
                        <td><a href='/Entry/Edit?id=${v.id}'</a>Изменить
                            <a href='/Entry/Delete?id=${v.id}'</a>Удалить</td>
                    </tr>`;
                });

                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
});



function changeCompleteStatus(guid) { //статус выполнения записи
    var status = $('input[name=' + guid +']').is(':checked'); 
    $.ajax({
        type: "POST",
        url: '/Entry/ChangeCompleteStatus',
        data:
            { currentStatus: status, id : guid },
        success: function () {
        },
        error: function (error) {
            alert(error.val);
        }
    });
};

function selectValueChanged() {
    var val = $("#select option:selected").text();
    if (val == "Памятка") {
        $("#dateEnd").prop("disabled", true);
        $("#timeEnd").prop("disabled", true);
        $("#endTimeValidation").prop("disabled", true);
    }
    else {
        $("#dateEnd").prop("disabled", false);
        $("#timeEnd").prop("disabled", false);
        $("#endTimeValidation").prop("disabled", false);
    }

    if (val == "Встреча") {
        $("#place").prop("disabled", false);
        $("#placeValidation").prop("disabled", false);
    }
    else {
        $("#place").prop("disabled", true);
        $("#placeValidation").prop("disabled", true);
    }
}

function filterByDate() {//скрываем записи, которые не проходят фильтр
    var dateFilter = $('#dateFilter').val();
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    today = yyyy + '-' + mm + '-' + dd;

    if (dateFilter == "День") {
        $('#tableBody tr').filter(function () {
            $(this).toggle($(this).text().indexOf(today) > -1)
        });
    }

    if (dateFilter == "Месяц") {
        $('#tableBody tr').filter(function () {
            $(this).toggle($(this).text().indexOf(yyyy + '-' + mm) > -1)
        });
    }

    if (dateFilter == "Список") {
        $('#tableBody tr').filter(function () {
            $(this).toggle($(this).text().indexOf('') > -1)
        });
    }

}

function searchFilter() { //скрываем записи, которые не проходят фильтр
    var search = $('#search').val().toLowerCase();
    $('#tableBody tr').filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(search) > -1)
    });
}