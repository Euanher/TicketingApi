import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  root: './', // Make sure the root is set properly
  publicDir: 'public', // Specify the public directory
  build: {
    outDir: 'dist',
    emptyOutDir: true,
  },
});
