import * as React from "react";
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css'; // Core grid CSS, always needed
import 'ag-grid-community/styles/ag-theme-alpine.css';
import {useRef, useState} from "react"; // Optional theme CSS


const CallsGrid = ({fetchData, data}) => {
    const gridRef = useRef(); // Optional - for accessing Grid's API
    

    // Each Column Definition results in one Column.
    const [columnDefs, setColumnDefs] = useState([
        {field: 'id', headerName: "№ запроса", width: 120},
        {field: 'createdBy', headerName: "Автор", width: 120},
        {field: 'waitingTime', headerName: "Время ожидания", width: 160},
        {field: 'executingTime', headerName: "Время исполнения", width: 170},
        {field: 'summaryTime', headerName: "Общее время", width: 130},
        {field: 'employeeName', headerName: "Исполнитель", width: 140},
        {
            field: 'status', headerName: "Статус", cellStyle: params => {
                if (params.value === 'Запрос выполнен') {
                    return {backgroundColor: '#00FF7F'};
                }
                if (params.value === 'Запрос выполняется') {
                    return {backgroundColor: '#FFD700'};
                }
                if (params.value === 'Запрос в очереди') {
                    return {backgroundColor: '#FA8072'};
                }
                return null;
            }
        }
    ]);
    
    
    return <div className="ag-theme-alpine" style={{height: "100%", width: "100%"}}>
                <AgGridReact
                    ref={gridRef} // Ref for accessing Grid's API
                    rowData={data} // Row Data for Rows
                    columnDefs={columnDefs} // Column Defs for Columns
                />
            </div>
}



export default CallsGrid;