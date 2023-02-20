import {BrowserRouter} from 'react-router-dom'
import {Routes, Route} from 'react-router'
import StartPage from './pages/StartPage/StartPage'
import Playground from './pages/Playground/Playground'
import {HubConnection} from '@microsoft/signalr'
import {useState} from 'react'

function App() {
  const [connection, setConnection] = useState<HubConnection>();

  return (
      <div className="App">
        <BrowserRouter>
          <Routes>
            <Route path='/' element={<StartPage/>}/>
            <Route path='/game' element={<Playground connection={connection}
                                    setConnection={(val: HubConnection) => setConnection(val)}/>}/>
          </Routes>
        </BrowserRouter>
      </div>
  )
}

export default App
