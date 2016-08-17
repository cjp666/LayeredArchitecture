var gulp = require('gulp');
var browserSync = require('browser-sync');
var ts = require('gulp-typescript');
var merge = require('merge2');

gulp.task('build-ts', function () {
    var tsResult = gulp.src([
        './typings/**/*.d.ts',
        './*.ts'
    ],
        { base: '.' })
        .pipe(ts({
            typescript: require('typescript'),
            declarationFiles: false,
            noExternalResolve: true,
            target: 'es5',
            module: 'amd',
            emitDecoratorMetadata: true
        }));

    return merge([
        tsResult.dts.pipe(gulp.dest('.')),
        tsResult.js.pipe(gulp.dest('.'))
    ]);
});

var path = {
    sourceTS: './*.ts',
    sourceJS: './*.js',
    html: './*.html',
    style: 'styles/**/*.css'
};

gulp.task('serve', function (done) {
    // browserSync({
    //     open: false,
    //     port: 9000,
    //     server: {
    //         baseDir: ['.'],
    //         middleware: function (req, res, next) {
    //             res.setHeader('Access-Control-Allow-Origin', '*');
    //             next();
    //         }
    //     }
    // }, done);

    browserSync.init([path.sourceJS, path.html, path.style], {
        port: 9000,
        server: {
            baseDir: '.'
        }
    });
});

function reportChange(event) {
    console.log('File ' + event.path + ' was ' + event.type + ', running tasks...');
}

gulp.task('watch', ['serve'], function () {
    gulp.watch(path.sourceTS, [browserSync.reload]).on('change', reportChange);
    gulp.watch(path.sourceJS, [browserSync.reload]).on('change', reportChange);
    gulp.watch(path.html, [browserSync.reload]).on('change', reportChange);
    gulp.watch(path.style, [browserSync.reload]).on('change', reportChange);
});
