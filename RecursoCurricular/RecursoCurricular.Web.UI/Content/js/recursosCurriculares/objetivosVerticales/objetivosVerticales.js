jQuery(document).ready(function () {

    var table;

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

    $(document).on('click', 'a[typebutton=Add]', function () {

        $.getJSON("/RecursosCurriculares/ObjetivoVertical/AddObjetivoVertical/" + $("#tipoEducacion").val() + "/" + $("#grado").val() + "/" + $("#sector").val(), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el sector", "error");
            }
            else {

                $('#tipoEducacionCodigo').val(data.TipoEducacionNombre);
                $('#gradoCodigo').val(data.GradoNombre);
                $('#sectorId').val(data.SectorNombre);
                $('#objetivoVerticalId').val(data.Id);
                $('#numero').val(data.Numero);
                $('#descripcion').val(data.Descripcion);

                popUp();
            }
        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON("/RecursosCurriculares/ObjetivoVertical/EditObjetivoVertical/" + $("#tipoEducacion").val() + "/" + $("#grado").val() + "/" + $("#sector").val() + "/" + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Se ha producido un error al cargar la información", "error");
            }
            else {

                $('#tipoEducacionCodigo').val(data.TipoEducacionNombre);
                $('#gradoCodigo').val(data.GradoNombre);
                $('#sectorId').val(data.SectorNombre);
                $('#objetivoVerticalId').val(data.Id);
                $('#numero').val(data.Numero);
                $('#descripcion').val(data.Descripcion);

                popUp();
            }
        })
    })

    $(document).on('click', 'a[typebutton=Delete]', function () {

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el objetivo vertical",
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
                    url: '/RecursosCurriculares/ObjetivoVertical/DeleteObjetivoVertical/' + $("#tipoEducacion").val() + "/" + $("#grado").val() + "/" + $("#sector").val() + "/" + id,
                    success: function (data) {

                        if (data === "200") {

                            table.ajax.reload();

                            swal("Eliminado!", "El objetivo vertical fue eliminado de forma correcta", "success");
                        }
                        else {

                            swal("Error!", "El objetivo vertical no puede ser eliminado", "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "El objetivo vertical no puede ser eliminado", "error");
                    }
                });
            });
    })

    var validator = $('#formModal').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            Nombre: {
                required: true
            },
            Descripcion: {
                required: true
            }
        },
        messages: {
            Nombre: {
                required: 'Ingrese el nombre'
            },
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
                tipoEducacionCodigo: $("#tipoEducacion").val(),
                gradoCodigo: $("#grado").val(),
                sectorId: $('#sector').val(),
                id: $('#objetivoVerticalId').val(),
                numero: $('#numero').val(),
                descripcion: $('#descripcion').val()
            };

            $.ajax({
                type: "POST",
                url: "/RecursosCurriculares/ObjetivoVertical/ObjetivosVerticales",
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
        "ajax": "/RecursosCurriculares/ObjetivoVertical/GetObjetivosVerticales/" + $('#tipoEducacion').val() + "/" + $('#grado').val() + "/" + $("#sector").val(),
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
                "searchable": false,
                "sortable": false
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

            if ($('#sector').val() !== "-1") {
                $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar objetivo vertical" typebutton="Add"><i class="fa fa-plus"></i></a>');
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