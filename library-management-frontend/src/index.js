import React, { createContext } from 'react';
import { createRoot } from 'react-dom/client';
import App from './App';
import UserStore from './store/UserStore';

export const Context = createContext(null);

createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Context.Provider value={{ user: new UserStore() }}>
      <App />
    </Context.Provider>
  </React.StrictMode>,
);
