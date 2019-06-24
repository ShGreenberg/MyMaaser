$(() => {
    $("#btn-givemaaser-modal").on('click', function () {
        const maaserGiven = {
            amount: $("#mg-amount").val(),
            toWhere: $("#mg-toWhere").val(),
            date: $("#mg-date").val()
        };
        $.post("/home/addmaasergiven", maaserGiven, function (id) {

            $.get("/home/lastmaasergiven", { id }, function (mg) {
                const day = new Date(mg.date);
                const date = day.getMonth() + 1 + "/" + day.getDate() + "/" + day.getFullYear();

                $("#mg-table").append(`<tr>
                <td>$${mg.amount.toFixed(2)}</td>
                <td>${mg.toWhere}</td>
                <td>${date}</td>
            </tr>`);

                $("#give-maaser-modal").modal('toggle');
                $("#mg-amount").val('');
                $("#mg-toWhere").val('');
                $("#mg-date").val('');
            });
        })

    });
});