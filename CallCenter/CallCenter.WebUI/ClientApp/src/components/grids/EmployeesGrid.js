import * as React from "react";
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css'; // Core grid CSS, always needed
import 'ag-grid-community/styles/ag-theme-alpine.css';
import {useCallback, useEffect, useRef, useState} from "react";
import {Button} from "reactstrap"; // Optional theme CSS


const EmployeesGrid = ({fetchData, data}) => {
    const gridRef = useRef(); 
    const [isAdding, setIsAdding] = useState(false)
    
    const positions = ['Operator', 'Manager', 'Director'];

    // Each Column Definition results in one Column.
    const [columnDefs, setColumnDefs] = useState([
        {field: 'id', headerName: "Личный номер", width: 140},
        {field: 'name', headerName: "Имя", width: 110, editable: true},
        {field: 'position', headerName: "Должность", width: 140, editable: true,
            cellEditor: 'agSelectCellEditor',
            cellEditorParams: {
                values: positions,
            },},
        {field: 'status', width: 180, headerName: "Статус", cellStyle: params => {
                if (params.value === 'Свободен') {
                    return {backgroundColor: '#00FF7F'};
                }
                else {
                    if (params.value) {
                        return {backgroundColor: '#FFD700'};
                    }
                    else {
                        return {backgroundColor: 'transparent'};
                    }
                }
            }
        },
        {
            field: '',
            width: 100,
            cellRenderer: (row) => <button onClick={() => deleteEmployee(row.data)}>Уволить</button>,
        }
    ]);

    const onCellValueChanged = useCallback((event) => {
        if (event.data.id) {
            updateEmployee(event.data);
        }
    }, []);
    
    const addEmployee = (data, gridApi) => {
        data.position = positions.indexOf(data.position) + 1;
        
        fetch('api/employees', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data) // body data type must match "Content-Type" header
        })
            //.then(result => result.json())
            .then(response => {
                gridApi.applyTransaction({ remove: [ data ]})
                fetchData();
                if (response.ok) {
                    return response.json();
                }
                
                response.text().then(function (text) {
                    alert(text);
                });
            });
    }

    const updateEmployee = (data) => {
        const pos = data.position;
        data.position = positions.indexOf(pos) + 1;
        fetch('api/employees', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data) // body data type must match "Content-Type" header
        })
            //.then(result => result.json())
            .then(response => {
                data.position = pos;
                if (response.ok) {
                    return response.json();
                }
                response.text().then(function (text) {
                    alert(text);
                });
            })
            
    }

    const deleteEmployee = (data) => {
        if (data.id) {
            fetch('api/employees/' + data.id, {
                method: 'DELETE'
            })
                //.then(result => result.json())
                .then(response => {
                    gridRef.current.api.applyTransaction({ remove: [ data ]})

                    if (response.ok) {
                        return response.json();
                    }
                    response.text().then(function (text) {
                        alert(text);
                    });
                })
            
        }
        
    }



    return <div className="ag-theme-alpine" style={{height: "90%", width: "100%"}}>
        <Button onClick={() => {
            const gridApi = gridRef.current.api;
            if (!isAdding) {
                gridApi.applyTransaction({ add: [{}]})
                setIsAdding(true);
            }
            else {
                const addingRow = gridApi.getDisplayedRowAtIndex(gridApi.getLastDisplayedRow());
                addEmployee(addingRow.data, gridApi);
                setIsAdding(false);
            }
        }}>{isAdding ? 'Сохранить нового работника' : 'Добавить работника'}</Button>
        <Button onClick={() => {
            const gridApi = gridRef.current.api;
            const addingRow = gridApi.getDisplayedRowAtIndex(gridApi.getLastDisplayedRow());
            gridApi.applyTransaction({ remove: [ addingRow.data ]})
            setIsAdding(false);
        }} style={!isAdding ? { display: 'none'} : {marginLeft: 10}}>Отменить добавление</Button>
        <AgGridReact
            ref={gridRef} 
            rowData={data} 
            columnDefs={columnDefs}
            onCellValueChanged={onCellValueChanged}
        />
    </div>
}



export default EmployeesGrid;