
        $(document).ready(function () {
            var dataTable = $('#table-org-list').DataTable({
                'order': [[1, "desc"]],
                'iDisplayLength': 25,
                'bFilter': false,
                'processing': true,
                'serverSide': true,
                'ajax': {
                    'url': '/Moderator/Organizations/GetOrganization',
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
                        { "data": "ShortDescription", "autoWidth": true },
                        { "data": "Classifier", "autoWidth": true },
                        { "data": "Id", "autoWidth": true },
                ]
            });

            //Фильтрация организации по названию
            $('#orgFilter').on('keyup', function () {
                dataTable.search(this.value).draw();
            })

            $('#table-org-list').on('draw.dt', function () {
                InitEditBtnForTable(dataTable);
            });

            function InitEditBtnForTable(dataTable) {
                $('#table-org-list tbody tr').each(function (i) {
                    var id = $(this).children('td:nth-child(1)').text();
                    var editBtn = '<a href="/Moderator/Organizations/Edit/' + id + '"  class="btn btn-icon-only green">' +
                                    '<span class="fa fa-edit"></span>' +
                               '</a>';
                    $(this).children('td:nth-child(7)').html(editBtn);

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
