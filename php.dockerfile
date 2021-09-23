FROM php:8.0.7-fpm                                                                
                                                                                
# PHP extensions                                                                
                                                                                
USER root                                                                       
                                                                                
RUN apt-get update && \                                                         
    apt-get install -y \                                                        
    libxml2-dev \                                                               
    libzip-dev \                                                                
    exiftool \                                                                  
    libpng-dev \                                                                
    libmagickwand-dev \                                                         
    zip \
    git \
    libonig-dev 

RUN apt-get update && apt-get install -y libmagickwand-dev --no-install-recommends && rm -rf /var/lib/apt/lists/*

# install imagick
# Version is not officially released https://pecl.php.net/get/imagick but following works for PHP 8
RUN mkdir -p /usr/src/php/ext/imagick; \
    curl -fsSL https://github.com/Imagick/imagick/archive/06116aa24b76edaf6b1693198f79e6c295eda8a9.tar.gz | tar xvz -C "/usr/src/php/ext/imagick" --strip 1; \
    docker-php-ext-install imagick;

ENV PHP_INI_MEMORY_LIMIT 256M
ENV PHP_INI_UPLOAD_FILESIZE 18M
ENV PHP_INI_GC_MAXLIFETIME 3600
ENV PHP_INI_POST_MAX_SIZE 18M
RUN echo 'memory_limit = ${PHP_INI_MEMORY_LIMIT}' >> /usr/local/etc/php/conf.d/docker-php-memlimit.ini                                                                               
RUN echo 'upload_max_filesize = ${PHP_INI_UPLOAD_FILESIZE}' >> /usr/local/etc/php/conf.d/docker-php-memlimit.ini
RUN echo 'session.gc_maxlifetime = ${PHP_INI_GC_MAXLIFETIME}' >> /usr/local/etc/php/conf.d/docker-php-memlimit.ini
RUN echo 'post_max_size = ${PHP_INI_POST_MAX_SIZE}' >> /usr/local/etc/php/conf.d/docker-php-memlimit.ini
RUN echo 'default_socket_timeout = 120' >> /usr/local/etc/php/conf.d/docker-php-memlimit.ini

# Install mbstring extension
RUN docker-php-ext-install mbstring
RUN docker-php-ext-enable mbstring 

RUN docker-php-ext-install zip                                                  
RUN docker-php-ext-install pdo pdo_mysql mysqli soap                            
RUN chown -R www-data:www-data /var/www/html                                    
                                                                                
RUN docker-php-ext-configure exif                                               
RUN docker-php-ext-install exif                                                 
RUN docker-php-ext-enable exif                                                  
                                                                                
RUN docker-php-ext-install gd                                                   


