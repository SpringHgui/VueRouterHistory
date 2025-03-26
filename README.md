# RouterHistory
[![Nuget](https://img.shields.io/nuget/v/RouterHistory)](https://www.nuget.org/packages/RouterHistory/)

用于单页面应用，使用History模式下，通过AspNetCore提供文件服务。

# 使用
1. 使用nuget安装`RouterHistory`包
2. 在`app.UseRouting`,`app.MapControllers`和`app.UseStaticFiles()` 之后添加`app.UseRouterHistory();`，以提供性能。

```c#
 // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();
    
    // 添加这一行即可
    app.UseRouterHistory();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
```
3. 将编译后的文件放到`wwwroot`目录内
4. 开始体验吧。

# 遇到问题
请提交[issue](https://github.com/SpringHgui/RouterHistory/issues/new)给作者
