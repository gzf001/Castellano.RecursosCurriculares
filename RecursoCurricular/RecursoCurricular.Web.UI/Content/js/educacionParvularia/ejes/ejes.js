﻿jQuery(document).ready(function () {

    var table;

    $('#ambito').change(function (e) {

        e.preventDefault();

        $("#nucleo").html("<option value='-1'>[Seleccione]</option>");
        $("#ciclo").html("<option value='-1'>[Seleccione]</option>");

        var ambito = $('#ambito').val();

        $.getJSON('/EducacionParvularia/Home/Nucleos/' + ambito, function (data) {

            var items = "";

            $.each(data, function (i, nucleo) {
                items += "<option value='" + nucleo.Value + "'>" + nucleo.Text + "</option>";
            });

            $("#nucleo").html(items);
        });

        gridView();
    })

    $('#nucleo').change(function (e) {

        e.preventDefault();

        $("#ciclo").html("<option value='-1'>[Seleccione]</option>");

        $.getJSON('/Educacion/Home/Ciclos/', function (data) {

            var items = "";

            $.each(data, function (i, ciclo) {
                items += "<option value='" + ciclo.Value + "'>" + ciclo.Text + "</option>";
            });

            $("#ciclo").html(items);
        });

        gridView();
    })

    $('#ciclo').change(function (e) {

        e.preventDefault();

        table = gridView();
    })

    $(document).on('click', 'a[typebutton=Add]', function () {

        $.getJSON("/EducacionParvularia/Eje/AddEje/" + $("#ambito").val() + "/" + $('#nucleo').val() + "/" + $('#ciclo').val(), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el ciclo", "error");
            }
            else {

                $('#ambitoCodigo').val(data.AmbitoExperienciaAprendizaje.Nombre);
                $('#nucleoId').val(data.NucleoAprendizaje.Nombre)
                $('#cicloCodigo').val(data.Ciclo.Nombre);
                $('#ejeId').val(data.Id);
                $('#numero').val(data.Numero);
                $('#nombre').val(data.Nombre);

                popUp();
            }
        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON("/EducacionParvularia/Eje/EditEje/" + $("#ambito").val() + "/" + $('#nucleo').val() + "/" + $('#ciclo').val() + "/" + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Se ha producido un error al cargar la información", "error");
            }
            else {

                $('#ambitoCodigo').val(data.AmbitoExperienciaAprendizaje.Nombre);
                $('#nucleoId').val(data.NucleoAprendizaje.Nombre)
                $('#cicloCodigo').val(data.Ciclo.Nombre);
                $('#ejeId').val(data.Id);
                $('#numero').val(data.Numero);
                $('#nombre').val(data.Nombre);

                popUp();
            }
        })
    })

    $(document).on('click', 'a[typebutton=Delete]', function () {

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el eje",
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
                    url: '/EducacionParvularia/Eje/DeleteEje/' + $("#ambito").val() + "/" + $('#nucleo').val() + "/" + $('#ciclo').val() + "/" + id,
                    success: function (data) {

                        if (data === "200") {

                            table.ajax.reload();

                            swal("Eliminado!", "El eje fue eliminado de forma correcta", "success");
                        }
                        else {

                            swal("Error!", "El eje no puede ser eliminado", "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "El eje no puede ser eliminado", "error");
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
            }
        },
        messages: {
            Nombre: {
                required: 'Ingrese el nombre'
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
                ambitoExperienciaAprendizajeCodigo: $('#ambito').val(),
                nucleoId: $('#nucleo').val(),
                cicloCodigo: $('#ciclo').val(),
                id: $('#ejeId').val(),
                numero: $('#numero').val(),
                nombre: $('#nombre').val()
            };

            $.ajax({
                type: "POST",
                url: "/EducacionParvularia/Eje/Ejes",
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
        "ajax": "/EducacionParvularia/Eje/GetEjes/" + $("#ambito").val() + "/" + $('#nucleo').val() + "/" + $('#ciclo').val(),
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
        "fnInitComplete": function (oSettings, json) {

            if ($('#ciclo').val() > 0) {
                $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar núcleo" typebutton="Add"><i class="fa fa-plus"></i></a>');
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