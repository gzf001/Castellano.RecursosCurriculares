jQuery(document).ready(function () {

    var table;

    $('#dimension').change(function (e) {

        e.preventDefault();

        table = gridView();
    })

    $(document).on('click', 'a[typebutton=Add]', function () {

        $.getJSON('/BasesCurriculares/ObjetivoAprendizajeOAT/AddObjetivoAprendizajeOAT/' + $("#dimension").val(), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione la dimensión", "error");
            }
            else {
                $('#objetivoOATId').val(data.Id);
                $('#dimensionId').val(data.DimensionOAT.Nombre)
                $('#numero').val(data.Numero);
                $('#nombre').val(data.Nombre);
                $('#descripcion').val(data.Descripcion);

                popUp();
            }
        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON('/BasesCurriculares/ObjetivoAprendizajeOAT/EditObjetivoAprendizajeOAT/' + $("#dimension").val() + '/' + $(this).attr('data-value'), function (data) {

            $('#objetivoOATId').val(data.Id);
            $('#dimensionId').val(data.DimensionOAT.Nombre)
            $('#numero').val(data.Numero);
            $('#nombre').val(data.Nombre);
            $('#descripcion').val(data.Descripcion);

            popUp();
        });
    })

    $(document).on('click', 'a[typebutton=Delete]', function () {

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el objetivo de aprendizaje transversal",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Cancelar",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si, eliminala",
            closeOnConfirm: false
        },
            function () {

                $.ajax({
                    type: 'GET',
                    url: '/BasesCurriculares/ObjetivoAprendizajeOAT/DeleteObjetivoAprendizajeOAT/' + $("#dimension").val() + '/' + id,
                    success: function (data) {

                        if (data === "200") {

                            table.ajax.reload();

                            swal("Eliminado!", "El objetivo de aprendizaje transversal fue eliminado de forma correcta", "success");
                        }
                        else {

                            swal("Error!", "El objetivo de aprendizaje transversal no puede ser eliminado", "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "El objetivo de aprendizaje transversal no puede ser eliminado", "error");
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
                dimensionOATId: $("#dimension").val(),
                id: $('#objetivoOATId').val(),
                numero: $('#numero').val(),
                nombre: $('#nombre').val(),
                descripcion: $('#descripcion').val()
            };

            $.ajax({
                type: "POST",
                url: "/BasesCurriculares/ObjetivoAprendizajeOAT/ObjetivosAprendizajeOAT",
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
        "ajax": "/BasesCurriculares/ObjetivoAprendizajeOAT/GetObjetivosAprendizajeOAT/" + $("#dimension").val(),
        "columns": [
            { "data": "Numero" },
            { "data": "Nombre" },
            { "data": "Descripcion" },
            { "data": "Accion" }
        ],
        "destroy": true,
        "order": [[0, "asc"]],
        "columnDefs": [
            {
                "targets": [2],
                "searchable": false,
                "sortable": false
            },
            {
                "targets": [3],
                "searchable": false,
                "sortable": false
            }
        ],
        "fnInitComplete": function (oSettings, json) {

            if ($('#dimension').val() !== '-1') {

                $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar objetivo de aprendizaje transversal" typebutton="Add"><i class="fa fa-plus"></i></a>');
            }
        },
        "iDisplayLength": 15,
        "aLengthMenu": [
            [15, 20, 25, 30, -1],
            [15, 20, 25, 30, "All"]
        ],
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