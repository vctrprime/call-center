import * as React from "react";
import {useEffect, useState} from "react";
import NumericInput from 'react-numeric-input';
import {Button} from "reactstrap";



const Settings = () => {
    const [settings, setSettings] = useState({})
    
    const [timeManagerTemp, setTimeManagerTemp] = useState(0)
    const [timeDirectorTemp, setTimeDirectorTemp] = useState(0)
    const [timeLeftLimitTemp, setTimeLeftLimitTemp] = useState(0)
    const [timeRightLimitTemp, setTimeRightLimitTemp] = useState(0)
    
    const [hasChanges, setHasChanges] = useState(false)
    
    useEffect(() => {
        fetchData();
    }, [])
    
    useEffect(() => {
        if (settings) {
            if (settings.timeManager !== timeManagerTemp ||
                settings.timeDirector !== timeDirectorTemp ||
                settings.executeTimeLimitLeft !== timeLeftLimitTemp ||
                settings.executeTimeLimitRight !== timeRightLimitTemp
            ) {
                setHasChanges(true);
            }
            else {
                setHasChanges(false);
            }
        }
    },[timeManagerTemp, timeDirectorTemp, timeLeftLimitTemp, timeRightLimitTemp])
    
    const fetchData = () => {
        fetch('api/settings')
            .then(result => result.json())
            .then(data => {
                setSettings(data);
                setTimeManagerTemp(data.timeManager);
                setTimeDirectorTemp(data.timeDirector);
                setTimeLeftLimitTemp(data.executeTimeLimitLeft);
                setTimeRightLimitTemp(data.executeTimeLimitRight);
                
            });
    }
    
    const reset = () => {
        setTimeManagerTemp(settings.timeManager);
        setTimeDirectorTemp(settings.timeDirector);
        setTimeLeftLimitTemp(settings.executeTimeLimitLeft);
        setTimeRightLimitTemp(settings.executeTimeLimitRight);
    }

    const update = () => {
        fetch('api/settings', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                timeManager: timeManagerTemp,
                timeDirector: timeDirectorTemp,
                executeTimeLimitLeft: timeLeftLimitTemp,
                executeTimeLimitRight: timeRightLimitTemp
            }) // body data type must match "Content-Type" header
        })
            .then((response) => {
                setHasChanges(false);
                fetchData();
                if (response.ok) {
                    return response.json();
                }
                response.text().then(function (text) {
                    alert(text);
                });
            })
        
    }
    
    return <div style={{width: '100%', height: '100%', padding: 15}}>
        <div className='setting-block'>
            <span>Время реакции менеджера:</span>
            <NumericInput onChange={(value) => setTimeManagerTemp(value)} style={{ input: {width: '60px'}}} min={20} max={80} value={timeManagerTemp}/>
        </div>
        <div className='setting-block'>
            <span>Время реакции директора:</span>
            <NumericInput onChange={(value) => setTimeDirectorTemp(value)} style={{ input: {width: '60px'}}} min={40} max={100} value={timeDirectorTemp}/>
        </div>
        
        <div className='setting-block' >
            <span>Мин. время разговора:</span>
            <NumericInput onChange={(value) => setTimeLeftLimitTemp(value)} style={{ input: {width: '60px'}}} min={10} max={90} value={timeLeftLimitTemp}/>
            
        </div>
        <div className='setting-block' >
            <span>Макс. время разговора:</span>
            <NumericInput onChange={(value) => setTimeRightLimitTemp(value)} style={{ input: {width: '60px'}}} min={30} max={120} value={timeRightLimitTemp}/>
        </div>
        <div className='settings-buttons'>
            <Button disabled={!hasChanges} onClick={update}>Сохранить изменения</Button>
            <Button style={{marginLeft: 15}} disabled={!hasChanges} onClick={reset}>Сбросить изменения</Button>
        </div>
        
    </div>;
}

export default Settings;