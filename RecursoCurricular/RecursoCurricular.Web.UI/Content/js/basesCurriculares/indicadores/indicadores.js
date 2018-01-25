jQuery(document).ready(function () {

    $('#indicadorForm').hide();

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

            $.getJSON('/BasesCurriculares/Home/Ejes/' + sectorId, function (data) {

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

        gridView();
    })

    $(document).on('click', 'a[typebutton=OtherAction]', function (e) {

        e.preventDefault();

        $.getJSON("/BasesCurriculares/Indicador/SelectObjetivo/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#eje').val() + "/" + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el objetivo de aprendizaje", "error");
            }
            else {

                $('#objetivoAprendizajeId').val(data.Id);
                $('#objetivoTipoEducacion').val(data.TipoEducacionNombre);
                $('#objetivoGrado').val(data.GradoNombre)
                $('#objetivoSector').val(data.SectorNombre);
                $('#objetivoEje').val(data.EjeNombre);
                $('#objetivoDescripcion').val(data.Descripcion);

                gridViewIndicador();

                $('#form').hide(500);

                $('#indicadorForm').show(500);
            }
        })
    })

    $(document).on('click', 'a[typebutton=Add]', function (e) {

        e.preventDefault();

        $.getJSON("/BasesCurriculares/Indicador/AddIndicador/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#eje').val() + "/" + $('#objetivoAprendizajeId').val(), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el objetivo de aprendizaje", "error");
            }
            else {

                $('#indicadorId').val(data.Id);
                $('#tipoEducacionCodigo').val(data.ObjetivoAprendizaje.TipoEducacion.Nombre);
                $('#gradoCodigo').val(data.ObjetivoAprendizaje.Grado.Nombre)
                $('#sectorId').val(data.ObjetivoAprendizaje.Sector.Nombre);
                $('#ejeId').val(data.ObjetivoAprendizaje.Eje.Numero + '.- ' + data.ObjetivoAprendizaje.Eje.Nombre);
                $('#objetivoDescripcionPopUp').val(data.ObjetivoAprendizaje.Numero + '.- ' + data.ObjetivoAprendizaje.Descripcion);
                $('#descripcion').val(data.Descripcion);
                $('#numero').val(data.Numero);

                popUp();
            }
        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON("/BasesCurriculares/Indicador/EditIndicador/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#eje').val() + "/" + $('#objetivoAprendizajeId').val() + "/" + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Se ha producido un error al cargar la información", "error");
            }
            else {

                $('#indicadorId').val(data.Id);
                $('#tipoEducacionCodigo').val(data.ObjetivoAprendizaje.TipoEducacion.Nombre);
                $('#gradoCodigo').val(data.ObjetivoAprendizaje.Grado.Nombre)
                $('#sectorId').val(data.ObjetivoAprendizaje.Sector.Nombre);
                $('#ejeId').val(data.ObjetivoAprendizaje.Eje.Numero + '.- ' + data.ObjetivoAprendizaje.Eje.Nombre);
                $('#objetivoDescripcionPopUp').val(data.ObjetivoAprendizaje.Numero + '.- ' + data.ObjetivoAprendizaje.Descripcion);
                $('#descripcion').val(data.Descripcion);
                $('#numero').val(data.Numero);

                popUp();
            }
        })
    })

    $(document).on('click', 'a[typebutton=Delete]', function () {

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el indicador",
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
                    url: '/BasesCurriculares/Indicador/DeleteIndicador/' + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#eje').val() + "/" + $('#objetivoAprendizajeId').val() + "/" + id,
                    success: function (data) {

                        if (data === "200") {

                            gridView();

                            gridViewIndicador();

                            swal("Eliminado!", "El indicador fue eliminado de forma correcta", "success");
                        }
                        else {

                            swal("Error!", "El indicador no puede ser eliminado", "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "El objetivo de aprendizaje no puede ser eliminado", "error");
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
                objetivoAprendizajeId: $('#objetivoAprendizajeId').val(),
                id: $('#indicadorId').val(),
                numero: $('#numero').val(),
                descripcion: $('#descripcion').val()
            };

            $.ajax({
                type: "POST",
                url: "/BasesCurriculares/Indicador/Indicadores",
                data: obj,
                success: function (data) {

                    if (data === "200") {

                        gridView();

                        gridViewIndicador();

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

    $('#form').show(500);

    $('#indicadorForm').hide(500);
})

$('#cancelPopUp').click(function (e) {

    e.preventDefault();

    $.magnificPopup.close();
})

function gridView() {

    $('#gridView').DataTable({
        "ajax": "/BasesCurriculares/Indicador/GetIndicadores/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#eje').val(),
        "columns": [
            { "data": "Numero" },
            { "data": "Descripcion" },
            { "data": "Indicadores" },
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
}

function gridViewIndicador() {

    $('#indicadorGridView').DataTable({
        "ajax": "/BasesCurriculares/Indicador/GetIndicadores/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#eje').val() + "/" + $('#objetivoAprendizajeId').val(),
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

            $("#indicadorGridView_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar indicador" typebutton="Add"><i class="fa fa-plus"></i></a>');
        },
        "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
    });
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