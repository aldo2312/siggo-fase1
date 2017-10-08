$(function () {
    $('#MainTable tbody tr td').on('click', function () {
        $(this).toggleClass('clickable');
    }).on('click', function () {
        var tdValor = '';
        var values = '';
        var idEditar = '';
        var LinkEditar = '';

        tdValor = (this).innerHTML;

        idEditar = $(tdValor).attr('Identificador');
        if (idEditar == 'Editar') {
            LinkEditar = $(tdValor).attr('href');
        }
        if (idEditar == 'Eliminar' || idEditar == 'Resetear') {

            return;
        }
        tr = $(this).parents('tr');

        var tds = $(tr).find('td');
        $.each(tds, function (index, item) {
            if (index == 0) {
                values = values + item.innerHTML;
            }

        });
        values = $(values).attr('href');
        location.href = (values);

        if (LinkEditar != '') {
            location.href = (LinkEditar);
        }
    });
});