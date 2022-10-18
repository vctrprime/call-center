import React, { useEffect, useState} from 'react';
import CallsGrid from "./grids/CallsGrid";
import {HubConnectionBuilder} from "@microsoft/signalr";
import EmployeesGrid from "./grids/EmployeesGrid";
import Settings from "./Settings";

export const Home =() => {
  const [connection, setConnection] = useState(null);

  const [calls, setCalls] = useState([]);
  const [employees, setEmployees] = useState([]);

  useEffect(() => {
    fetchCalls();
    fetchEmployees();
      
    const connect = new HubConnectionBuilder()
        .withUrl("/hubs/calls")
        .withAutomaticReconnect()
        .build();
    setConnection(connect);
  }, []);

    useEffect(() => {
        if (connection) {
            connection
                .start()
                .then(() => {
                    connection.on("SendMessageToClient", (message) => {
                        fetchCalls();
                        fetchEmployees();
                    });
                })
                .catch((error) => console.log(error));
        }
    }, [connection]);
  
  const fetchCalls = () => {
      fetch('api/calls')
          .then(result => result.json())
          .then(data => setCalls(data));
  }
  
  const fetchEmployees = () => {
      fetch('api/employees')
          .then(result => result.json())
          .then(data => setEmployees(data));
  }
  
  return (
      <div style={{height: "100%", width: "100%"}}>
          <div style={{height: "100%", width: "60%", float: 'left'}}>
              <CallsGrid fetchData={fetchCalls} data={calls}/>
          </div>
          <div style={{height: "calc(100% - 170px)", width: "40%", float: 'left'}}>
              <EmployeesGrid fetchData={fetchEmployees} data={employees}/>
          </div>
          <div style={{width: "40%", float: 'left'}}>
              <Settings/>
          </div>
          
          
      </div>
  );
}

