import * as React from "react";
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css'; // Core grid CSS, always needed
import 'ag-grid-community/styles/ag-theme-alpine.css';
import {useEffect, useRef, useState} from "react"; // Optional theme CSS


const EmployeesGrid = ({fetchData, data}) => {
    const gridRef = useRef(); 

    // Each Column Definition results in one Column.
    const [columnDefs, setColumnDefs] = useState([
        {field: 'id', headerName: "Личный номер", width: 140},
        {field: 'name', headerName: "Имя"},
        {field: 'position', headerName: "Должность"},
        {field: 'status', headerName: "Статус", cellStyle: params => {
                if (params.value === 'Свободен') {
                    return {backgroundColor: '#00FF7F'};
                }
                else {
                    return {backgroundColor: '#FFD700'};
                }
            }
        },
    ]);
    

    return <div className="ag-theme-alpine" style={{height: "100%", width: "100%"}}>
        <AgGridReact
            ref={gridRef} // Ref for accessing Grid's API
            rowData={data} // Row Data for Rows
            columnDefs={columnDefs} // Column Defs for Columns
        />
    </div>
}



export default EmployeesGrid;