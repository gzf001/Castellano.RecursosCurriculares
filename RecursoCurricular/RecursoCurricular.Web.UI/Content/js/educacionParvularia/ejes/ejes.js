jQuery(document).ready(function () {

    $('#ambito').change(function (e) {

        e.preventDefault();

        $("#nucleo").html("<option value='-1'>[Seleccione]</option>");
        $("#ciclo").html("<option value='-1'>[Seleccione]</option>");

        var ambito = $('#ambito').val();

        $.getJSON('/EducacionParvularia/Eje/Nucleos/' + ambito, function (data) {

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

        $.getJSON('/EducacionParvularia/Eje/Ciclos/', function (data) {

            var items = "";

            $.each(data, function (i, ciclo) {
                items += "<option value='" + ciclo.Value + "'>" + ciclo.Text + "</option>";
            });

            $("#ciclo").html(items);
        });

        gridView();
    })

    $('#ciclo').change(function (e) {

        gridView();

        $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar núcleo" typebutton="Add"><i class="fa fa-plus"></i></a>');
    })

    $(document).on('click', 'a[typebutton=Add]', function () {

        $.getJSON('/EducacionParvularia/Nucleo/AddNucleo/' + $("#ambito").val(), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el ámbito de aprendizaje", "error");
            }
            else {

                $('#nucleoId').val(data.Id);
                $('#ambitoCodigo').val(data.AmbitoExperienciaAprendizaje.Nombre)
                $('#numero').val(data.Numero);
                $('#nombre').val(data.Nombre);

                popUp();
            }
        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON('/EducacionParvularia/Nucleo/EditNucleo/' + $("#ambito").val() + '/' + $(this).attr('data-value'), function (data) {

            $('#nucleoId').val(data.Id);
            $('#ambitoCodigo').val(data.AmbitoExperienciaAprendizaje.Nombre)
            $('#numero').val(data.Numero);
            $('#nombre').val(data.Nombre);

            popUp();
        });
    })

    $(document).on('click', 'a[typebutton=Delete]', function () {

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el núcleo",
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
                    url: '/EducacionParvularia/Nucleo/DeleteNucleo/' + $("#ambito").val() + '/' + id,
                    success: function (data) {

                        if (data === "200") {

                            table.ajax.reload();

                            swal("Eliminado!", "El núcleo fue eliminado de forma correcta", "success");
                        }
                        else {

                            swal("Error!", "El núcleo no puede ser eliminado", "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "El núcleo no puede ser eliminado", "error");
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
                id: $('#nucleoId').val(),
                ambitoExperienciaAprendizajeCodigo: $("#ambito").val(),
                numero: $('#numero').val(),
                nombre: $('#nombre').val()
            };

            $.ajax({
                type: "POST",
                url: "/EducacionParvularia/Nucleo/Nucleos",
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

      $('#gridView').DataTable({
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