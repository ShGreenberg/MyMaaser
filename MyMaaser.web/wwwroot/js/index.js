$(() => {
    $("#btn-addmoney-modal").on('click', function () {
        const money = {
            amount: $("#amount").val(),
            amountLeft: $("#amount").val(),
            recievedFrom: $("#from").val(),
            date: $("#date").val()
        }
        $.post("/home/addmoney", money, function ({ money, total }) {
            $("#for-errors").empty();
            $("#amount").val("");
            $("#from").val("");
            $("#date").val("");45
            $("#add-money-modal").modal("toggle");
            const day = new Date(money.date);
            const date = day.getMonth() + 1 + "/" + day.getDate() + "/" + day.getFullYear();
            if (money === "error") {
                $("#for-errors").append('<span style="color: red">Error- entered something incorrectly</span>')
                return;
            }
            

            $("#maaser-table").append(`<tr>
                <td>$${money.amount.toFixed(2)}</td>
                <td>${money.recievedFrom}</td>
                <td>${date}</td>
                <td>${money.paidUp}</td>
            </tr>`);

            $("#owe").text(`still need to take maaser from $${total.toFixed(2)} - which means giving $${(total / 10).toFixed(2)}`);
        });
    });

    //$("#btn-givemaaser-modal").on('click', function () {
    //    const maaserGiven = {
    //        amount: $("#mg-amount").val(),
    //        toWhere: $("#mg-toWhere").val(),
    //        date: $("#mg-date").val()
    //    };
    //    console.log("hi");
    //    $.post("/home/addmaasergiven", maaserGiven, function () {
    //        console.log("Hi");
    //        const x = $("#total-num-given").val();
    //        $("#total-num-given").val(x+1);
    //    });
    //});
});