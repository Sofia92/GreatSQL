module.exports = {
    server: {
        middleware: [
            function (req, res, next) {
                var url = req.url;
                if (!url.endsWith(".js"))
                    next();
                
            }
        ]
    }
};