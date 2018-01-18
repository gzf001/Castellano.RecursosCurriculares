jQuery(document).ready(function () {

    var table = gridView();

    $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar dimensión" typebutton="Add"><i class="fa fa-plus"></i></a>');

    $(document).on('click', 'a[typebutton=Add]', function () {

        $.getJSON('/Tic/Dimension/AddDimension', function (data) {

            $('#dimensionId').val(data.Id);
            $('#numero').val(data.Numero);
            $('#nombre').val(data.Nombre);
            $('#descripcion').val(data.Descripcion);

            popUp();
        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON('/Tic/Dimension/EditDimension/' + $(this).attr('data-value'), function (data) {

            $('#dimensionId').val(data.Id);
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
            text: "Se eliminará la dimensión",
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
                    url: '/Tic/Dimension/DeleteDimension/' + id,
                    success: function (data) {

                        if (data === "200") {

                            table.ajax.reload();

                            swal("Eliminado!", "La dimensión fue eliminada de forma correcta", "success");
                        }
                        else {

                            swal("Error!", "La aplicación no puede ser eliminada", "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "La aplicación no puede ser eliminada", "error");
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
                id: $('#dimensionId').val(),
                numero: $('#numero').val(),
                nombre: $('#nombre').val(),
                descripcion: $('#descripcion').val()
            };

            $.ajax({
                type: "POST",
                url: "/Tic/Dimension/Dimensiones",
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
        "ajax": "/Tic/Dimension/GetDimensiones",
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