import { AppConfig } from '@shared/infastructure/config';
import React from 'react';
import { createRoot } from 'react-dom/client';
import { App } from './App';
import './index.css';

console.log(AppConfig.serverEndpoint);

const root = createRoot(document.getElementById('root')!);
root.render(<App />);