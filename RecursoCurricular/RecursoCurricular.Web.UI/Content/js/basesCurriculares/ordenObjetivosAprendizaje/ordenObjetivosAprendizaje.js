jQuery(document).ready(function () {

    var table;

    $('#divForm').hide();

    $('#tipoEducacion').change(function (e) {

        e.preventDefault();

        $("#grado").html("<option value='-1'>[Seleccione]</option>");
        $("#sector").html("<option value='-1'>[Seleccione]</option>");

        var tipoEducacion = $('#tipoEducacion').val();

        $.getJSON('/Educacion/Home/Grados/' + tipoEducacion, function (data) {

            var items = "";

            $.each(data, function (i, grado) {
                items += "<option value='" + grado.Value + "'>" + grado.Text + "</option>";
            });

            $("#grado").html(items);
        });

        gridView();
    })

    $('#grado').change(function (e) {

        e.preventDefault();

        $("#sector").html("<option value='-1'>[Seleccione]</option>");
        $("#eje").html("<option value='-1'>[Seleccione]</option>");

        if ($('#grado').val() > 0) {

            $.getJSON('/Educacion/Home/Sectores/', function (data) {

                var items = "";

                $.each(data, function (i, sector) {
                    items += "<option value='" + sector.Value + "'>" + sector.Text + "</option>";
                });

                $("#sector").html(items);
            });
        }

        gridView();
    })

    $('#sector').change(function (e) {

        e.preventDefault();

        table = gridView();
    })

    $(document).on('click', 'a[typebutton=Edit]', function (e) {

        e.preventDefault();

        $.getJSON("/BasesCurriculares/Unidad/EditUnidad/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + '/' + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el sector", "error");
            }
            else {

                var unidadId = data.Id;

                $('#tipoEducacionCodigo').val(data.TipoEducacionNombre);
                $('#gradoCodigo').val(data.GradoNombre)
                $('#sectorId').val(data.SectorNombre);
                $('#unidadId').val(unidadId);
                $('#numero').val(data.Numero);
                $('#nombre').val(data.Nombre);

                $.ajax({
                    url: "/BasesCurriculares/OrdenObjetivoAprendizaje/ObjetivosAprendizaje/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + '/' + unidadId,
                    method: "GET",
                    processData: false,
                    contentType: false,
                }).done(function (partialView) {

                    $('#unidadObjetivoAprendizaje').html(partialView);

                    $('#form').hide(500);

                    $('#divForm').show(500);
                });
            }
        });
    })

    $('#save').click(function (e) {

        e.preventDefault();

        var ordenes = [];

        $('.itemPadre').each(function (i, objetivo) {

            var orden = {
                ejeId: $(objetivo.childNodes[1]).val(),
                objetivoAprendizajeId: $(objetivo.childNodes[2]).val(),
                indicadores:[]
            }

            $(objetivo.childNodes[3].childNodes[0].childNodes).each(function (j, indicador) {

                orden.indicadores.push($(indicador.childNodes[1]).val());

            });

            ordenes.push(orden);
        });

        var obj = {
            tipoEducacionCodigo: $('#tipoEducacion').val(),
            gradoCodigo: $('#grado').val(),
            sectorId: $('#sector').val(),
            unidadId: $('#unidadId').val(),
            ordenes: ordenes
        };

        $.ajax({
            type: "POST",
            url: "/BasesCurriculares/OrdenObjetivoAprendizaje/OrdenObjetivoAprendizaje",
            data: obj,
            success: function (data) {

                if (data === "200") {


                    swal("Listo!", "El orden fue aplicado correctamente", "success");
                }
                else {

                    swal("Error!", "Se ha producido un error al ordenar información", "error");
                }
            },
            error: function (data) {

                swal("Error!", "Se ha producido un error al ordenar información", "error");
            }
        });
    });
})

$('#cancel').click(function (e) {

    e.preventDefault();

    $('#form').show(500);

    $('#divForm').hide(500);
})

function gridView() {

    var table = $('#gridView').DataTable({
        "ajax": "/BasesCurriculares/Unidad/GetUnidades/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val(),
        "columns": [
            { "data": "Numero" },
            { "data": "Nombre" },
            { "data": "Accion" }
        ],
        "destroy": true,
        "order": [[0, "asc"]],
        "columnDefs": [
            {
                "targets": [0],
                "searchable": true,
                "sortable": true
            },
            {
                "targets": [1],
                "searchable": true,
                "sortable": true
            },
            {
                "targets": [2],
                "searchable": false,
                "sortable": false
            }
        ],
        "iDisplayLength": 15,
        "aLengthMenu": [
            [15, 20, 25, 30, -1],
            [15, 20, 25, 30, "All"]
        ],
        "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
    });

    return table;
}