const { merge } = require('webpack-merge');
const commonConfig = require('./webpack.common.js');

const port = process.env.PORT || 3000;

module.exports = merge(commonConfig, {
    mode: 'development',
    devtool: 'inline-source-map',
    devServer: {
        host: 'localhost',
        port: port,
        historyApiFallback: true,
        open: true,
    }
});