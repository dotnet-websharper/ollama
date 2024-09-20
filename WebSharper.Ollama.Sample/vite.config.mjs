import { defineConfig } from 'vite'
import path from "path";

// https://vitejs.dev/config/
export default defineConfig({
    root: path.resolve('wwwroot'),
  plugins: [],
  optimizeDeps:{
    
  },
  build: {
    outDir: "dist"
    },
    server: {
        proxy: {
            '/api': {
                target: 'http://localhost:11434'
            }
        },
        port: 5555
    }
})
