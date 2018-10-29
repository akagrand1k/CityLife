
        $(document).ready(function () {
            var dataTable = $('#table-org-list').DataTable({
                'order': [[1, "desc"]],
                'iDisplayLength': 25,
                'bFilter': false,
                'processing': true,
                'serverSide': true,
                'ajax': {
                    'url': '/Root/Organizations/GetOrganization',
                    'data': {
                        'OrgName': function () { return $('#OrgName').val(); },
                    }
                },
                "language": {
                    //   "url": "http://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Russian.json"
                    "url": "/Scripts/datatable/russian.json"


                },
                "columns": [
                        { "data": "Id", "autoWidth": true, },
                        { "data": "Name", "autoWidth": true },
                        { "data": "SmallPathImage", "autoWidth": true, },
                        { "data": "DateReg", "autoWidth": true, },
                        { "data": "IsActive", "autoWidth": true, },
                        { "data": "ShortDescription", "autoWidth": true },
                        { "data": "Classifier", "autoWidth": true },
                        { "data": "Id", "autoWidth": true },
                ]
            });

      

            $('#table-org-list').on('draw.dt', function () {
                InitEditBtnForTable(dataTable);
            });

            function InitEditBtnForTable(dataTable) {
                $('#table-org-list tbody tr').each(function (i) {
                    var id = $(this).children('td:nth-child(1)').text();
                    var editBtn = '<a href="/Root/Organizations/Edit/' + id + '"  class="btn btn-icon-only green">' +
                                    '<span class="fa fa-edit"></span>' +
                               '</a>';

                    var deleteBtn = '<a href="/Root/Organizations/Delete/' + id + '"  class="btn btn-icon-only red">' +
                                    '<span class="fa fa-trash-o"></span>' +
                               '</a>';

                    $(this).children('td:nth-child(8)').html(editBtn + deleteBtn);

                    var logoPath = $(this).children('td:nth-child(3)').text();
                    var logo = '<img src="' + logoPath + '"/>'
                    $(this).children('td:nth-child(3)').html(logo);
                });
                isGet = false;
            }


            $("#filter-button").click(function (ev) {
                ReloadTable();
            });

            function ReloadTable() {
                dataTable.ajax.reload();
                dataTable.ajax.draw();
            }

        });
