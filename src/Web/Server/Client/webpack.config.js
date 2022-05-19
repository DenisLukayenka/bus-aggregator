const webpack = require('webpack');
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

const port = process.env.PORT || 3000;
const mode = process.env.NODE_ENV || "development";

module.exports = {
    mode: mode,
    entry: path.join(__dirname, '/src/index.tsx'),
    output: {
        path: path.resolve(__dirname, '..', 'wwwroot'),
        filename: 'bundle.js',
    },
    devtool: 'inline-source-map',
    module: {
        rules: [
            {
                test: /\.?(js|jsx)$/,
                exclude: /node_modules/,
                use: ['babel-loader']
            },
            {
                test: /\.?(ts|tsx)$/,
                exclude: /node_modules/,
                use: ['ts-loader']
            },
            {
                test: /\.?css$/i,
                use: ['style-loader', 'css-loader']
            },
            { test: /\.json$/, type: 'json' }
        ],
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: path.join(__dirname, 'public', 'index.html')
        })
    ],
    devServer: {
        host: 'localhost',
        port: port,
        historyApiFallback: true,
        open: true,
    },
    resolve: {
        extensions: ['.js', '.ts', '.jsx', '.tsx', '.json', '.css', '.tsx', '.ts'],
        modules: [
            path.resolve(__dirname, "./"),
            path.resolve(__dirname, "node_modules"),
        ],
        alias: {
            '@static': path.resolve(__dirname, './src/static'),
            '@shared': path.resolve(__dirname, './src/shared'),
            '@core': path.resolve(__dirname, './src/core'),
        },
    }
};