jQuery(document).ready(function () {

    var table;

    $('#tipoEducacion').change(function (e) {

        e.preventDefault();

        $("#grado").html("<option value='-1'>[Seleccione]</option>");
        $("#sector").html("<option value='-1'>[Seleccione]</option>");
        $("#eje").html("<option value='-1'>[Seleccione]</option>");

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

        $("#eje").html("<option value='-1'>[Seleccione]</option>");

        var sectorId = $('#sector').val();

        if (sectorId !== '-1') {

            $.getJSON('/RecursosCurriculares/Home/Ejes?sectorId=' + sectorId, function (data) {

                var items = "";

                $.each(data, function (i, sector) {
                    items += "<option value='" + sector.Value + "'>" + sector.Text + "</option>";
                });

                $("#eje").html(items);
            });
        }

        gridView();
    })

    $('#eje').change(function (e) {

        e.preventDefault();

        table = gridView();
    })

    $(document).on('click', 'a[typebutton=Add]', function () {

        $.getJSON("/RecursosCurriculares/Contenido/AddContenido/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#eje').val(), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el eje", "error");
            }
            else {

                $('#tipoEducacionCodigo').val(data.TipoEducacion.Nombre);
                $('#gradoCodigo').val(data.Grado.Nombre)
                $('#sectorId').val(data.Sector.Nombre);
                $('#ejeId').val(data.Eje.Nombre);
                $('#contenidoId').val(data.Id);
                $('#numero').val(data.Numero);
                $("#transversal").prop("checked", data.Transversal);
                $('#descripcion').val(data.Descripcion);

                popUp();
            }
        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON("/RecursosCurriculares/Contenido/EditContenido/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#eje').val() + "/" + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Se ha producido un error al cargar la información", "error");
            }
            else {

                $('#tipoEducacionCodigo').val(data.TipoEducacion.Nombre);
                $('#gradoCodigo').val(data.Grado.Nombre)
                $('#sectorId').val(data.Sector.Nombre);
                $('#ejeId').val(data.Eje.Nombre);
                $('#contenidoId').val(data.Id);
                $('#numero').val(data.Numero);
                $("#transversal").prop("checked", data.Transversal);
                $('#descripcion').val(data.Descripcion);

                popUp();
            }
        })
    })

    $(document).on('click', 'a[typebutton=Delete]', function () {

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el contenido",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Cancelar",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si, eliminalo",
            closeOnConfirm: false
        },
            function () {

                $.ajax({
                    type: 'GET',
                    url: '/RecursosCurriculares/Contenido/DeleteContenido/' + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#eje').val() + "/" + id,
                    success: function (data) {

                        if (data === "200") {

                            table.ajax.reload();

                            swal("Eliminado!", "El contenido fue eliminado de forma correcta", "success");
                        }
                        else {

                            swal("Error!", "El contenido no puede ser eliminado", "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "El contenido no puede ser eliminado", "error");
                    }
                });
            });
    })

    var validator = $('#formModal').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            Descripcion: {
                required: true
            }
        },
        messages: {
            Descripcion: {
                required: 'Ingrese la descripción'
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).closest('.field').addClass(errorClass).removeClass(validClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest('.field').removeClass(errorClass).addClass(validClass);
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element.parent());
        },
        submitHandler: function (form) {

            var obj = {
                tipoEducacionCodigo: $('#tipoEducacion').val(),
                gradoCodigo: $('#grado').val(),
                sectorId: $('#sector').val(),
                ejeId: $('#eje').val(),
                id: $('#contenidoId').val(),
                numero: $('#numero').val(),
                transversal: $("#transversal").prop("checked"),
                descripcion: $('#descripcion').val()
            };

            $.ajax({
                type: "POST",
                url: "/RecursosCurriculares/Contenido/Contenidos",
                data: obj,
                success: function (data) {

                    if (data === "200") {

                        table.ajax.reload();

                        $.magnificPopup.close();

                        swal("Listo!", "Su información fue guardada correctamente", "success");
                    }
                    else {

                        swal("Error!", "Se ha producido un error al registrar la información", "error");
                    }
                },
                error: function (data) {

                    swal("Error!", "Se ha producido un error al registrar la información", "error");
                }
            });
        }
    })
})

$('#cancel').click(function (e) {

    e.preventDefault();

    $.magnificPopup.close();
})

function gridView() {

    var table = $('#gridView').DataTable({
        "ajax": "/RecursosCurriculares/Contenido/GetContenidos/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#eje').val(),
        "columns": [
            { "data": "Numero" },
            { "data": "Descripcion" },
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
        "fnInitComplete": function (oSettings, json) {

            if ($('#eje').val() !== "-1") {
                $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar contenido" typebutton="Add"><i class="fa fa-plus"></i></a>');
            }
        },
        "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
    });

    return table;
}

function popUp() {

    var form = $('#modal-form');

    form.find(".state-error").removeClass("state-error");
    form.find(".state-success").removeClass("state-success");
    form.find("em").remove();

    $.magnificPopup.open({
        removalDelay: 500,
        items: {
            src: "#modal-form"
        },
        callbacks: {
            beforeOpen: function (e) {

                this.st.mainClass = "mfp-flipInX";
            }
        },
        midClick: true
    });
}