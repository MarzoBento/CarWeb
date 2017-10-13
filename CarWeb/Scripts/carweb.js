$(document).ready(function () {
    $("#txtPesquisa").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '@Url.Action("Index", "Download")',
                type: "POST",
                dataType: "json",
                data: { pesquisaLote: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.nome, value: item.nome };
                    }))

                }
            })
        },
        messages: {
            noResults: "", results: ""
        },
    });
})

function getCheckedValues() {

    return Array.from(document.querySelectorAll('input[type="checkbox"]'))
        .filter((checkbox) => checkbox.checked)
        .map((checkbox) => checkbox.value);
}

document.getElementById('btnDownload').addEventListener('click', () => {
    downloadAll(getCheckedValues());
});

function downloadAll(urls) {
    var link = document.createElement('a');

    //link.setAttribute('download', null);
    link.style.display = 'none';

    document.body.appendChild(link);

    for (var i = 0; i < urls.length; i++) {
        link.setAttribute('href', urls[i]);
        var filename = urls[i].replace(/^.*[\\\/]/, '');
        link.setAttribute('download', filename);
        link.click();
    }

    document.body.removeChild(link);
}
