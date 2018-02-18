jQuery(document).ready(function () {

    var table;

    $('#sector').change(function (e) {

        e.preventDefault();

        table = gridView();
    })

    $(document).on('click', 'a[typebutton=Add]', function () {

        $.getJSON("/RecursosCurriculares/Eje/AddEje?sectorId=" + $("#sector").val(), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el sector", "error");
            }
            else {

                $('#ejeId').val(data.Id);
                $('#sectorId').val(data.Sector.Nombre);
                $('#numero').val(data.Numero)
                $('#nombre').val(data.Nombre);

                $('#formModal input[type=checkbox]').each(function () {

                    $(this).prop('checked', false);
                });


                popUp();
            }
        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON("/RecursosCurriculares/Eje/EditEje/" + $("#sector").val() + "/" + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Se ha producido un error al cargar la información", "error");
            }
            else {

                $('#ejeId').val(data.Id);
                $('#sectorId').val(data.Sector.Nombre)
                $('#numero').val(data.Numero);
                $('#nombre').val(data.Nombre);

                $.each(data.SelectedTipoEducacion, function (i, item) {

                    $('[value=' + item + ']').prop('checked', true);

                });

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
                    url: '/RecursosCurriculares/Eje/DeleteEje/' + $("#sector").val() + "/" + id,
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
            },
            SelectedTipoEducacion: {
                tipoEducacionSelected: true
            }
        },
        messages: {
            Nombre: {
                required: 'Ingrese el nombre'
            },
            SelectedTipoEducacion: {
                tipoEducacionSelected: 'Seleccione al menos un tipo de educación'
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

            var selectedTipoEducacion = [];

            $(":checkbox:checked").each(function () {
                selectedTipoEducacion.push($(this).attr('value'));
            });

            var obj = {
                sectorId: $('#sector').val(),
                id: $('#ejeId').val(),
                numero: $('#numero').val(),
                nombre: $('#nombre').val(),
                selectedTipoEducacion: selectedTipoEducacion
            };

            $.ajax({
                type: "POST",
                url: "/RecursosCurriculares/Eje/Ejes",
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

    jQuery.validator.addMethod("tipoEducacionSelected", function (value, element, param) {

        var selectedTipoEducacion = $("input[name='SelectedTipoEducacion']:checked");

        if (selectedTipoEducacion.length > 0) {
            return true;
        }
        else {
            return false;
        }
    })
})

$('#cancel').click(function (e) {

    e.preventDefault();

    $.magnificPopup.close();
})

function gridView() {

    var table = $('#gridView').DataTable({
        "ajax": "/RecursosCurriculares/Eje/GetEjes?sectorId=" + $("#sector").val(),
        "columns": [
            { "data": "Numero" },
            { "data": "Nombre" },
            { "data": "TipoEducacionNombre" },
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

            if ($('#sector').val() !== "-1") {
                $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar eje" typebutton="Add"><i class="fa fa-plus"></i></a>');
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