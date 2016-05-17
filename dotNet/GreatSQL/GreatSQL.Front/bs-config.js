var pathExists = require('path-exists');

module.exports = {
    server: {
        middleware: {
            /**
             * 对 js 文件进行拦截
             * 以提供 ts 动态编译支持
             */
            2: function (req, res, next) {
                var url = '.' + req.url;

                // 不是以 js 结尾的直接跳过
                if (!url.endsWith('.js')) {
                    next();
                    return;
                }

                // 检查文件是否存在
                pathExists(url).then(exists => {

                    // 如果存在，直接跳过
                    if (exists) {
                        next();
                        return;
                    }

                    // 否则编译返回
                    console.log('test2 => ' + url);
                });
            }
        }
    }
};