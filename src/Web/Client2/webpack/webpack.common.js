const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    entry: path.join(__dirname, '../src/index.js'),
    output: {
        path: path.resolve(__dirname, '..', '..', 'wwwroot'),
        filename: '[name].bundle.js',
        clean: true
    },
    mode: 'production',
    plugins: [
        new HtmlWebpackPlugin({
            template: path.join(__dirname, '..', 'index.html'),
        })
    ],
}